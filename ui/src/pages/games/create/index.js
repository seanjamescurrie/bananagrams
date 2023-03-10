import {
  Box,
  Button,
  Container,
  Stack,
  Stepper,
  Typography,
} from "@mui/material";
import Step from "@mui/material/Step";
import StepLabel from "@mui/material/StepLabel";
import { Fragment, useState } from "react";
import { useNavigate } from "react-router-dom";
import Icon from "../../../components/icon";
import { SelectPlayers, DefineRules, ReviewGame } from "./components";
import { useCreateGame } from "../../../contexts/create-game-context";

const steps = ["Search users", "Create rules", "Review"];

const CreateGame = ({ type }) => {
  const game = useCreateGame();
  const [activeStep, setActiveStep] = useState(0);
  const navigate = useNavigate();

  const handleNext = () => {
    activeStep === steps.length - 1
      ? createGame()
      : setActiveStep((prevActiveStep) => prevActiveStep + 1);
  };

  const handleBack = () => {
    setActiveStep((prevActiveStep) => prevActiveStep - 1);
  };

  async function createGame() {
    const response = await fetch("http://localhost:5016/games", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        gameAnagramTypeId: game.state.gameAnagramTypeId,
        playerIds: game.state.userIds,
        title: game.state.title,
        totalAnagrams: game.state.totalAnagrams,
        totalAttempts: game.state.totalAttempts,
      }),
    });
    if (response.status === 201) {
      let data = await response.json();
      let newGameId = data.id;

      console.log(data);
      console.log("success");
      navigate(`/games/${newGameId}/lobby`);
    } else {
      console.log("error");
    }
  }

  return (
    <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
      <Typography variant="h2" sx={{ mb: 0 }}>
        Create Face Off
      </Typography>

      <Typography variant="p" color={"text.secondary"}>
        Go head to head with friends in a set of timed challenges
      </Typography>

      <Box sx={{ width: "100%", mt: 5 }}>
        <Stepper activeStep={activeStep}>
          {steps.map((label, index) => {
            const stepProps = {};
            const labelProps = {};
            return (
              <Step key={label} {...stepProps}>
                <StepLabel {...labelProps}>{label}</StepLabel>
              </Step>
            );
          })}
        </Stepper>

        <Fragment>
          {activeStep === 0 ? (
            <SelectPlayers></SelectPlayers>
          ) : activeStep === 1 ? (
            <DefineRules></DefineRules>
          ) : (
            <ReviewGame></ReviewGame>
          )}
          <Stack direction="row" justifyContent="space-between" sx={{ mt: 2 }}>
            <Button
              disabled={activeStep === 0}
              onClick={handleBack}
              variant="outlined"
              sx={{
                mb: 2,
              }}
            >
              <Icon.ArrowLeftIcon />
              Back
            </Button>
            <Button
              onClick={handleNext}
              variant="contained"
              sx={{
                mb: 2,
              }}
              disabled={game.state.userIds <= 0}
            >
              {activeStep === steps.length - 1 ? "Finish" : "Continue"}

              {activeStep < steps.length - 1 ? (
                <Icon.ArrowRightIcon></Icon.ArrowRightIcon>
              ) : (
                ""
              )}
            </Button>
          </Stack>
        </Fragment>
      </Box>
    </Container>
  );
};

export default CreateGame;
