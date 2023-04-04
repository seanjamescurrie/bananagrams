import { Typography, Button, Box, Slide } from "@mui/material";
import { Container } from "@mui/system";
import { useRef, useState } from "react";
import { OpenLobbies, CompletedGames } from "./components";
import { Icon } from "../../components";

const Games = () => {
  const [viewCompleted, setViewCompleted] = useState(false);
  const [checked, setChecked] = useState(false);

  const handleChange = () => {
    setChecked((prev) => !prev);

    setViewCompleted(!viewCompleted);
  };

  const containerRef = useRef(null);

  return (
    <>
      <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
        {viewCompleted ? (
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

        {
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
        }
      </Container>
    </>
  );
};

export default Games;
