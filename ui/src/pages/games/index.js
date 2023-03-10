import {
  Typography,
  Button,
  Box,
  Slide,
  FormControlLabel,
  Switch,
  Grow,
  Collapse,
  Paper,
  Grid,
} from "@mui/material";
import { Container } from "@mui/system";
import { useEffect, useRef, useState, TransitionGroup } from "react";
import { Loader } from "../../components";
import { OpenLobbies, CompletedGames } from "./components";
import { Icon } from "../../components";

const Games = () => {
  const [viewCompleted, setViewCompleted] = useState(false);
  const [games, setGames] = useState([]);
  const [checked, setChecked] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  const handleChange = () => {
    setChecked((prev) => !prev);

    setViewCompleted(!viewCompleted);
  };

  const containerRef = useRef(null);

  // const fetchData = async () => {
  //   const response = await fetch("http://localhost:5016/games", {
  //     method: "GET",
  //   });
  //   if (response.status === 200) {
  //     const data = await response.json();
  //     let foundGames = data.map((game) => ({
  //       id: game.id,
  //       title: game.title,
  //       dateCreated: game.dateCreated,
  //       users: game.gameUsers.map((user) => ({
  //         username: user.username,
  //       })),
  //     }));
  //     setGames(foundGames);
  //   }
  //   setIsLoading(false);
  // };

  // useEffect(() => {
  //   fetchData();
  // }, []);

  function UpdateList() {
    setViewCompleted(!viewCompleted);
  }

  return (
    <>
      <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
        {!isLoading ? (
          <Loader></Loader>
        ) : viewCompleted ? (
          <>
            <Button
              onClick={handleChange}
              variant="outlined"
              size="small"
              sx={{
                height: "2rem",
                mb: 2,
                borderRadius: "90px",
                paddingRight: 2,
              }}
            >
              <Icon.ArrowLeftIcon /> View Lobbies
            </Button>
            <Box>
              <Typography variant="h2">Completed Games</Typography>
            </Box>
            <Typography variant="p" color={"text.secondary"}>
              Game details of completed games
            </Typography>
          </>
        ) : (
          <>
            <Button
              onClick={handleChange}
              variant="outlined"
              size="small"
              sx={{
                height: "2rem",
                mb: 2,
                borderRadius: "90px",
                paddingLeft: 2,
              }}
            >
              View Completed Games
              <Icon.ArrowRightIcon />
            </Button>
            <Typography variant="h2">Lobbies</Typography>
            <Typography variant="p" color={"text.secondary"}>
              Games lobbies waiting on players
            </Typography>
          </>
        )}

        {!isLoading ? (
          <Loader></Loader>
        ) : (
          <Box
            sx={{
              display: "flex",
              justifyContent: "center",
              overflow: "hidden",
              width: 1,
              m: "auto",
            }}
            ref={containerRef}
          >
            <Slide
              direction="left"
              in={checked}
              container={containerRef.current}
              mountOnEnter
              unmountOnExit
            >
              <Box
                sx={{
                  position: "absolute",
                  display: "flex",
                  width: 0.8,
                }}
              >
                <CompletedGames />
              </Box>
            </Slide>

            <Slide
              direction="right"
              in={!checked}
              container={containerRef.current}
              mountOnEnter
              unmountOnExit
            >
              <Box sx={{ position: "absolute", width: 0.8, display: "flex" }}>
                <OpenLobbies />
              </Box>
            </Slide>
          </Box>
        )}
      </Container>
    </>
  );
};

export default Games;
