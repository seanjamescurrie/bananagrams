import { Avatar, Box, Divider, Grid, Stack, Typography } from "@mui/material";
import { Container } from "@mui/system";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Loader } from "../../../components";

const Lobby = () => {
  const params = useParams();

  const [game, setGame] = useState({});
  const [timer, setTimer] = useState(10);
  const [isLoading, setIsLoading] = useState(true);

  const navigate = useNavigate();
  const playersReady = false;

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
        })),
      };
      setGame(foundGame);
      console.log("success");
    } else {
      console.log("error");
    }
    setIsLoading(false);
  };

  useEffect(() => {
    getGame();
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
    if (timer <= 0) navigate("/games/1");
  }, [timer]);

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
                {game.users.map((user) => (
                  <Box sx={{ m: "auto", mt: 1 }}>
                    <Avatar sx={{ m: "auto", mb: 1 }}></Avatar>
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
        </>
      )}
    </Container>
  );
};

export default Lobby;
