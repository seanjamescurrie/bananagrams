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

  const containerRef = useRef(null);

  async function fetchData() {
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
  }

  const content = <CompletedGames></CompletedGames>;

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
          control={<Switch checked={viewCompleted} onChange={UpdateList} />}
          label="Show from target"
        />
        <Slide
          direction="up"
          in={viewCompleted}
          container={containerRef.current}
        >
          {content}
        </Slide> */}
      </Container>
    </>
  );
};

export default Games;
