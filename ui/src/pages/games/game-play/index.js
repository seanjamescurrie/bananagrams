import { Button, Container, Grid, Stack, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";
import {
  AnagramDisplayField,
  AnagramInputField,
  PlayerStatus,
  Timer,
  LoadingScreen,
} from "./components";

const Game = ({ title, description, type }) => {
  const date = new Date();
  const currentDate = `${date.getDate()} / ${date.getMonth()} / ${date.getFullYear()}`;

  const anagramSolution = "LYCHEE";
  const anagram = "CHEELY";
  const dailyAttempts = 5;
  const attemptsMade = 1;

  type = "faceOff";

  const [timer, setTimer] = useState(0);

  useEffect(() => {
    console.log(timer);
    timer > 0 && setTimeout(() => setTimer(timer - 1), 1000);
  }, [timer]);

  return (
    <>
      {timer > 0 ? (
        <LoadingScreen></LoadingScreen>
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
                  Daily Anagram
                </Typography>
                <Typography variant="p" gutterBottom color={"text.secondary"}>
                  {currentDate}
                </Typography>

                <Box sx={{ mt: 5 }}>
                  <Typography variant="h5" sx={{ mb: 0 }}>
                    Challenge
                  </Typography>
                  <AnagramDisplayField anagram={anagram}></AnagramDisplayField>
                </Box>

                <Box sx={{ mt: 5 }}>
                  <Typography variant="h5" sx={{ mb: 0 }}>
                    Attempts
                  </Typography>

                  <AnagramInputField
                    anagram={anagram}
                    isDisabled={false}
                    isAttempt={true}
                  ></AnagramInputField>

                  <AnagramInputField
                    anagram={anagram}
                    isDisabled={true}
                    isAttempt={true}
                  ></AnagramInputField>

                  <AnagramInputField
                    anagram={anagram}
                    isDisabled={true}
                    isAttempt={true}
                  ></AnagramInputField>

                  <AnagramInputField
                    anagram={anagram}
                    isDisabled={true}
                    isAttempt={true}
                  ></AnagramInputField>

                  <AnagramInputField
                    anagram={anagram}
                    isDisabled={true}
                    isAttempt={true}
                  ></AnagramInputField>
                </Box>

                <Box sx={{ width: 1 / 4, m: "auto", mt: 5 }}>
                  <Stack>
                    <Button
                      // onClick={() => navigate("/")}
                      variant="contained"
                      sx={{
                        mb: 2,
                      }}
                    >
                      Submit
                    </Button>
                    <Button
                      variant="outlined"
                      width="lg"
                      sx={{
                        mb: 2,
                      }}
                    >
                      Clear
                    </Button>
                  </Stack>
                </Box>
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
