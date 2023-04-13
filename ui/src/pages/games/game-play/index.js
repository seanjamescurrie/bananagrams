import { Container, Grid, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";
import {
  PlayerStatus,
  Timer,
  LoadingScreen,
  AnagramHandler,
} from "./components";
import { useGamePlay } from "../../../contexts/game-play-context";
import { useParams } from "react-router-dom";
import { GameService } from "../../../services";
import { LoginUtils } from "../../../utils";
import { AuthContext } from "../../../contexts";
import { HubConnectionBuilder } from "@microsoft/signalr";

const Game = () => {
  const params = useParams();
  const typeOrId = params.id;
  const { authState } = AuthContext.useLogin();
  const currentUserId = LoginUtils.getAccountId(authState.accessToken);

  const { gamePlayState, gamePlayDispatch } = useGamePlay();

  const [timer, setTimer] = useState(5);
  const [game, setGame] = useState({});
  const [loading, setLoading] = useState(true);
  const [connection, setConnection] = useState();
  const [gameId, setGameId] = useState();
  const [initialiseGameId, setInitialiseGameId] = useState(true);

  const fetchData = async () => {
    // gamePlayDispatch({ type: "resetContext" });

    const response = await GameService.getById(typeOrId);
    if (response.status == 200) {
      const data = await response.json();

      console.log(currentUserId);
      let foundGame = {
        id: data.id,
        title: data.title,
        gameType: {
          id: data.gameAnagramType.id,
          title: data.gameAnagramType.title,
          maxAttempts: data.gameAnagramType.maxAttempts,
          timeAllowed: data.gameAnagramType.timeAllowed,
        },
        gameUsers: data.gameUsers.map((user) => ({
          id: user.id,
          username: user.username,
          gameAnagrams: data.gameAnagrams.map((anagram) => ({
            id: anagram.id,
            gameUserGameAnagrams: anagram.gameUserGameAnagrams
              .filter((guga) => guga.userId === user.id)
              .map((guga) => ({
                datePlayed: guga.datePlayed,
                dateSolved: guga.dateSolved,
              })),
          })),
        })),
        gameAnagrams: data.gameAnagrams.map((gameAnagram, i) => ({
          anagramWord: gameAnagram.anagramWord,
          gameId: gameAnagram.gameId,
          id: gameAnagram.id,
          order: gameAnagram.order,
          wordId: gameAnagram.wordId,
          isDisabled: true,
          isSolved: !gameAnagram.gameUserGameAnagrams.filter(
            (guga) => guga.userId == currentUserId
          )[0].dateSolved
            ? false
            : true,
          gameUserGameAnagrams: gameAnagram.gameUserGameAnagrams
            .filter((guga) => guga.userId == currentUserId)
            .map((guga) => ({
              id: guga.id,
              attempts: guga.attempts,
              datePlayed: guga.datePlayed,
              dateSolved: guga.dateSolved,
            })),
        })),
      };
      setGame(foundGame);

      gamePlayDispatch({
        type: "initialiseGameUsers",
        payload: {
          value: data.gameUsers.map((user) => ({
            id: user.id,
            username: user.username,
          })),
        },
      });

      setLoading(false);
    }
  };

  useEffect(() => {
    console.log("fetching");
    fetchData();
  }, [loading, gamePlayState.anagramRow, gamePlayState.activeAnagramIndex]);

  useEffect(() => {
    if (initialiseGameId && game.id !== undefined) {
      setGameId(game.id);
      setInitialiseGameId(false);
    }
  }, [game]);

  useEffect(() => {
    console.log(gameId);
  }, [gameId]);

  useEffect(() => {
    timer > 0 && setTimeout(() => setTimer(timer - 1), 1000);
  }, [timer]);

  useEffect(() => {
    if (typeOrId !== "daily" && gameId) {
      const connect = new HubConnectionBuilder()
        .withUrl(
          `http://localhost:5016/hub/game?userId=${currentUserId}&gameId=${gameId}`
        )
        .withAutomaticReconnect()
        .build();

      setConnection(connect);
      return () => {
        connect.stop();
      };
    }
  }, [gameId]);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("connected");
          connection.on("SendUpdate", (updateUserAttempt) => {
            console.log(updateUserAttempt);
            // gamePlayDispatch({
            //   type: "updateMultiplayerUserAttempts",
            //   payload: {
            //     value: updateUserAttempt,
            //   },
            // });
          });
        })
        .catch((error) => console.log(error));
    }
  }, [connection]);

  return (
    <>
      {loading || timer > 0 ? (
        <LoadingScreen type={typeOrId}></LoadingScreen>
      ) : (
        <Container maxWidth={false}>
          {typeOrId !== "daily" ? (
            <Timer
              totalAnagrams={game.gameAnagrams.length}
              gameId={game.id}
            ></Timer>
          ) : (
            <></>
          )}
          <Grid container spacing={3}>
            <Grid item xs={1}>
              {/* {typeOrId !== "daily" ? (
                <Box
                  sx={{ mt: 2, display: "flex", justifyContent: "flex-start" }}
                >
                  <PlayerStatus user={gamePlayState.users[0]}></PlayerStatus>
                </Box>
              ) : (
                <></>
              )} */}
            </Grid>
            <Grid item xs={10}>
              <Container sx={{ textAlign: "center", mt: 5 }}>
                <Typography variant="h2" gutterBottom sx={{ mb: 0 }}>
                  {game.title}
                </Typography>
                <Typography variant="p" gutterBottom color={"text.secondary"}>
                  {typeOrId === "daily" ? "Daily" : "Face Off"}
                </Typography>

                <AnagramHandler game={game}></AnagramHandler>
              </Container>
            </Grid>
            <Grid item xs={1}>
              {typeOrId !== "daily" ? (
                <>
                  {game.gameUsers.map((user) => (
                    <Box
                      sx={{
                        mt: 2,
                        display: "flex",
                        justifyContent: "flex-end",
                      }}
                      key={user.id}
                    >
                      <PlayerStatus user={user}></PlayerStatus>
                    </Box>
                  ))}
                  {/* {[...Array(gamePlayState.users.length)].map((x, i) => (
                    <Box
                      sx={{
                        mt: 2,
                        display: "flex",
                        justifyContent: "flex-end",
                      }}
                    >
                      <PlayerStatus
                        user={gamePlayState.users[i]}
                      ></PlayerStatus>
                    </Box>
                  ))} */}
                </>
              ) : (
                <></>
              )}
            </Grid>
          </Grid>
        </Container>
      )}
    </>
  );
};

export default Game;
