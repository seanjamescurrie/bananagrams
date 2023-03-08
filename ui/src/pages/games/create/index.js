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
import { Fragment, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import Icon from "../../../components/icon";
import { SelectPlayers, DefineRules, ReviewGame } from "./components";
import { CreateGameContext } from "../../../contexts/game-context";

const steps = ["Search users", "Create rules", "Review"];

const CreateGame = ({ type }) => {
  const [newGame, setNewGame] = useState({
    users: [],
    rules: { title: "", totalAnagrams: 0, totalAttempts: 0 },
  });
  const game = useMemo(() => ({ newGame, setNewGame }), [newGame]);

  const [activeStep, setActiveStep] = useState(0);
  const navigate = useNavigate();

  const handleNext = () => {
    activeStep === steps.length - 1
      ? navigate("/games/1/lobby")
      : setActiveStep((prevActiveStep) => prevActiveStep + 1);
  };

  const handleBack = () => {
    setActiveStep((prevActiveStep) => prevActiveStep - 1);
  };

  const handleReset = () => {
    setActiveStep(0);
  };

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
        {activeStep === steps.length ? (
          <Fragment>
            <Typography sx={{ mt: 2, mb: 1 }}>
              All steps completed - you&apos;re finished
            </Typography>
            <Box sx={{ display: "flex", flexDirection: "row", pt: 2 }}>
              <Box sx={{ flex: "1 1 auto" }} />
              <Button onClick={handleReset}>Reset</Button>
            </Box>
          </Fragment>
        ) : (
          <Fragment>
            <CreateGameContext.Provider value={game}>
              {activeStep === 0 ? (
                <SelectPlayers></SelectPlayers>
              ) : activeStep === 1 ? (
                <DefineRules></DefineRules>
              ) : (
                <ReviewGame></ReviewGame>
              )}
            </CreateGameContext.Provider>
            <Stack
              direction="row"
              justifyContent="space-between"
              sx={{ mt: 2 }}
            >
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
        )}
      </Box>
    </Container>
  );
};

export default CreateGame;
