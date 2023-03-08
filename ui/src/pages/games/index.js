import {
  Typography,
  Button,
  Box,
  Slide,
  FormControlLabel,
  Switch,
} from "@mui/material";
import { Container } from "@mui/system";
import { useEffect, useRef, useState } from "react";
import { OpenLobbies, CompletedGames } from "./components";

const Games = () => {
  const [viewCompleted, setViewCompleted] = useState(false);
  const [games, setGames] = useState([]);

  const [checked, setChecked] = useState(false);

  const handleChange = () => {
    setChecked((prev) => !prev);
  };

  const containerRef = useRef(null);

  const fetchData = async () => {
    const response = await fetch("http://localhost:5016/games", {
      method: "GET",
    });
    if (response.status === 200) {
      const data = await response.json();
      let foundGames = data.map((game) => ({
        id: game.id,
        title: game.title,
        dateCreated: game.dateCreated,
        users: game.gameUsers.map((user) => ({
          username: user.username,
        })),
      }));
      setGames(foundGames);
    }
  };

  // const content = <CompletedGames></CompletedGames>;
  const completedGames = <CompletedGames></CompletedGames>;
  const openLobbies = <OpenLobbies games={games}></OpenLobbies>;

  useEffect(() => {
    fetchData();
  }, []);

  function UpdateList() {
    setViewCompleted(!viewCompleted);
  }

  return (
    <>
      <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
        {viewCompleted ? (
          <>
            <Button
              onClick={UpdateList}
              variant="outlined"
              sx={{
                mb: 2,
              }}
            >
              View Lobbies
            </Button>
            <Box>
              <Typography variant="h2">Completed Games</Typography>
              <Typography variant="p" color={"text.secondary"}>
                Game details of completed games
              </Typography>
            </Box>
            <CompletedGames></CompletedGames>
          </>
        ) : (
          <>
            <Button
              onClick={UpdateList}
              variant="outlined"
              sx={{
                mb: 2,
              }}
            >
              View Completed Games
            </Button>
            <Typography variant="h2">Lobbies</Typography>
            <Typography variant="p" color={"text.secondary"}>
              Games lobbies waiting on players
            </Typography>
            <OpenLobbies games={games}></OpenLobbies>
          </>
        )}

        {/* <FormControlLabel
          control={
            // <Button
            //   onClick={handleChange}
            //   variant="outlined"
            //   sx={{
            //     mb: 2,
            //   }}
            // >
            //   {checked ? "View Open Lobbies" : "View Completed Games"}
            // </Button>
            <Switch checked={checked} onChange={handleChange} />
          }
          label="target"
        />
        <Slide direction="left" in={checked} container={containerRef.current}>
          <>{completedGames}</>
        </Slide>

        <Slide direction="right" in={!checked} container={containerRef.current}>
          <>{openLobbies}</>
        </Slide> */}
      </Container>
    </>
  );
};

export default Games;
