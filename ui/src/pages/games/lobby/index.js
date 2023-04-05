import { HubConnectionBuilder } from "@microsoft/signalr";
import {
  Avatar,
  Badge,
  Box,
  Divider,
  Grid,
  Stack,
  Typography,
} from "@mui/material";
import { Container } from "@mui/system";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Loader, Notification } from "../../../components";
import { useGamePlay } from "../../../contexts/game-play-context";
import { AuthContext } from "../../../contexts";
import { LoginUtils, FetchUtils } from "../../../utils";

const Lobby = () => {
  const params = useParams();
  const { authState } = AuthContext.useLogin();
  const { gamePlayState, gamePlayDispatch } = useGamePlay();

  const [game, setGame] = useState({});
  const [users, setUsers] = useState([]);
  const [timer, setTimer] = useState(10);
  const [isLoading, setIsLoading] = useState(true);
  const [connection, setConnection] = useState();
  const [playersReady, setPlayersReady] = useState(false);
  const [userJoinedLobby, setUserJoinedLobby] = useState(false);
  const [notification, setNotification] = useState("");

  const currentUserId = LoginUtils.getAccountId(authState.accessToken);
  const navigate = useNavigate();

  const getGame = async () => {
    const response = await fetch(`http://localhost:5016/games/${params.id}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });
    if (response.status === 200) {
      let data = await response.json();
      let foundGame = {
        id: data.id,
        title: data.title,
        totalAnagrams: data.gameAnagrams.length,
        users: data.gameUsers.map((user) => ({
          username: user.username,
          ready: false,
        })),
      };
      setGame(foundGame);
      setUsers(foundGame.users);
      console.log("success");
    } else {
      console.log("error");
    }
  };

  useEffect(() => {
    getGame();

    setIsLoading(false);
  }, []);

  useEffect(() => {
    console.log(game);
  }, [game]);

  useEffect(() => {
    if (playersReady) {
      const timer = setInterval(() => {
        setTimer((prevProgress) => (prevProgress <= 0 ? 10 : prevProgress - 1));
      }, 800);
      return () => {
        clearInterval(timer);
      };
    }
  }, [playersReady]);

  useEffect(() => {
    if (timer <= 0) navigate(`/games/${params.id}`);
  }, [timer]);

  useEffect(() => {
    if (game.id) {
      const connect = new HubConnectionBuilder()
        .withUrl(
          `http://localhost:5016/hub/game?userId=${currentUserId}&gameId=${game.id}`
        )
        .withAutomaticReconnect()
        .build();
      setConnection(connect);

      return () => {
        connect.stop();
      };
    }
  }, [game]);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("connected");
          connection.on("StartGame", (gameId) => {
            gamePlayDispatch({
              type: "startGame",
              payload: {
                value: gameId,
              },
            });
            console.log("start game");
          });
          connection.on("UserJoinedLobby", (message) => {
            console.log(message);
            setNotification(message + " joined lobby");
            setUserJoinedLobby(true);
            const newUserList = users.map((user) => ({
              username: user.username,
              ready: user.username === message ? true : user.ready,
            }));
            setUsers(newUserList);
          });
        })
        .catch((error) => console.log(error));
    }
  }, [connection]);

  useEffect(() => {
    if (gamePlayState.startMultiplayerGameId == game.id) {
      setPlayersReady(true);
    }
  }, [gamePlayState]);

  const showNotification = (notification) => {
    return <Notification message={notification} display={true}></Notification>;
  };

  return (
    <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
      {isLoading ? (
        <Loader></Loader>
      ) : timer <= 3 ? (
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: "space-around",
            height: "80vh",
          }}
        >
          <Stack spacing={2}>
            <Typography variant="h5">Game starting in</Typography>
            <Typography variant="h1" sx={{ fontSize: "20rem" }}>
              {timer}
            </Typography>
          </Stack>
        </Box>
      ) : (
        <>
          <Typography variant="body1" color={"text.secondary"} sx={{ mb: 1 }}>
            Lobby
          </Typography>
          <Typography variant="h2" sx={{ mb: 0 }}>
            {game.title}
          </Typography>

          {!playersReady ? (
            <Typography variant="p" color={"text.secondary"}>
              Waiting for other players to join
            </Typography>
          ) : (
            <></>
          )}

          <Typography variant="h4" sx={{ mt: 5 }}>
            Rules
          </Typography>

          <Box textAlign="left" sx={{ m: "auto", mt: 0, width: 1 / 2 }}>
            <Grid container spacing={2} sx={{ mt: 2 }}>
              <Grid item xs={8}>
                <Typography variant="h5"> Number of anagrams</Typography>
              </Grid>
              <Grid item xs={4}>
                <Typography variant="h4" sx={{ textAlign: "right" }}>
                  {game.totalAnagrams}
                </Typography>
              </Grid>
              <Grid item xs={8}>
                <Typography variant="h5">Number of attempts allowed</Typography>
              </Grid>
              <Grid item xs={4}>
                <Typography variant="h4" sx={{ textAlign: "right" }}>
                  3
                </Typography>
              </Grid>
              <Grid item xs={12} sx={{ mb: 0 }}>
                <Divider />
              </Grid>

              <Grid item xs={8}>
                <Typography variant="h5" color={"text.secondary"}>
                  Time limit
                </Typography>
              </Grid>
              <Grid item xs={4}>
                <Typography
                  variant="h4"
                  color={"text.secondary"}
                  sx={{ textAlign: "right" }}
                >
                  30 sec
                </Typography>
              </Grid>
            </Grid>

            <Divider />

            <Box sx={{ mt: 5, mb: 4, textAlign: "center" }}>
              <Typography variant="h4" sx={{ mb: 2 }}>
                Users
              </Typography>
              <Stack direction="row">
                {users.map((user) => (
                  <Box sx={{ m: "auto", mt: 1 }} key={user.username}>
                    <Badge
                      variant="dot"
                      overlap="circular"
                      color={user.ready ? "success" : "error"}
                      anchorOrigin={{
                        vertical: "top",
                        horizontal: "right",
                      }}
                    >
                      <Avatar sx={{ m: "auto", mb: 1 }}></Avatar>
                    </Badge>
                    <Typography variant="body1">{user.username}</Typography>
                  </Box>
                ))}
              </Stack>
            </Box>
          </Box>

          {playersReady ? (
            <Box
              sx={{
                position: "absolute",
                right: "60px",
                bottom: "60px",
              }}
            >
              <Stack
                direction="row"
                sx={{ display: "flex", alignItems: "flex-end" }}
              >
                <Typography variant="body1" color={"text.secondary"}>
                  Game starting in
                </Typography>

                <Typography
                  id="outlined-basic"
                  variant="h4"
                  sx={{ ml: 1, width: "1em", lineHeight: 0.8 }}
                >
                  {timer}
                </Typography>
              </Stack>
            </Box>
          ) : (
            <></>
          )}

          {userJoinedLobby && showNotification(notification)}
          {/* {notifications.map((note) => (
            <Notification message={note} display={true}></Notification>
          ))} */}
        </>
      )}
    </Container>
  );
};

export default Lobby;
