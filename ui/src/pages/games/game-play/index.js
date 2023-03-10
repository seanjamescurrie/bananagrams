import { Container, Grid, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useMemo, useState } from "react";
import {
  PlayerStatus,
  Timer,
  LoadingScreen,
  AnagramHandler,
} from "./components";
import { AnagramRowContext } from "../../../contexts/game-context";
import {
  GamePlayProvider,
  useGamePlay,
} from "../../../contexts/game-play-context";

const Game = ({ type }) => {
  type = "Daily";

  // const [anagramRow, setAnagramRow] = useState(0);
  // const row = useMemo(() => ({ anagramRow, setAnagramRow }), [anagramRow]);
  const gamePlayContext = useGamePlay();

  const [timer, setTimer] = useState(5);
  const [game, setGame] = useState({});
  const [loading, setLoading] = useState(true);

  const fetchData = async () => {
    const response = await fetch("http://localhost:5016/games/daily/1", {
      method: "GET",
    });
    if (response.status == 200) {
      const data = await response.json();
      let foundGame = {
        id: data.id,
        title: data.title,
        gameType: {
          id: data.gameAnagramType.id,
          title: data.gameAnagramType.title,
          maxAttempts: data.gameAnagramType.maxAttempts,
          timeAllowed: data.gameAnagramType.timeAllowed,
        },
        gameAnagrams: data.gameAnagrams.map((gameAnagram) => ({
          anagramWord: gameAnagram.anagramWord,
          gameId: gameAnagram.gameId,
          id: gameAnagram.id,
          order: gameAnagram.order,
          wordId: gameAnagram.wordId,
          isDisabled: true,
          isSolved:
            gameAnagram.gameUserGameAnagrams[0].dateSolved == null
              ? false
              : true,
          gameUserGameAnagrams: gameAnagram.gameUserGameAnagrams.map(
            (guga) => ({
              id: guga.id,
              attempts: guga.attempts,
              datePlayed: guga.datePlayed,
              dateSolved: guga.dateSolved,
            })
          ),
        })),
      };
      setGame(foundGame);
      console.log(foundGame);
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, [loading, gamePlayContext]);

  useEffect(() => {
    timer > 0 && setTimeout(() => setTimer(timer - 1), 1000);
  }, [timer]);

  return (
    <>
      {loading || timer > 0 ? (
        <LoadingScreen type={type}></LoadingScreen>
      ) : (
        <Container maxWidth={false}>
          {type === "faceOff" ? <Timer></Timer> : <></>}
          <Grid container spacing={3}>
            <Grid item xs={1}>
              {type === "faceOff" ? (
                <Box
                  sx={{ mt: 2, display: "flex", justifyContent: "flex-start" }}
                >
                  <PlayerStatus></PlayerStatus>
                </Box>
              ) : (
                <></>
              )}
            </Grid>
            <Grid item xs={10}>
              <Container sx={{ textAlign: "center", mt: 5 }}>
                <Typography variant="h2" gutterBottom sx={{ mb: 0 }}>
                  {game.title}
                </Typography>
                <Typography variant="p" gutterBottom color={"text.secondary"}>
                  {type}
                </Typography>
                {/* <AnagramRowContext.Provider value={row}> */}
                <AnagramHandler game={game}></AnagramHandler>
                {/* </AnagramRowContext> */}
              </Container>
            </Grid>
            <Grid item xs={1}>
              {type === "faceOff" ? (
                <>
                  <Box
                    sx={{ mt: 2, display: "flex", justifyContent: "flex-end" }}
                  >
                    <PlayerStatus></PlayerStatus>
                  </Box>
                  <Box
                    sx={{ mt: 2, display: "flex", justifyContent: "flex-end" }}
                  >
                    <PlayerStatus></PlayerStatus>
                  </Box>
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
