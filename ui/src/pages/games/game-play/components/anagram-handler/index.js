import { Button, Snackbar, Stack, Typography } from "@mui/material";
import MuiAlert from "@mui/material/Alert";
import { Box } from "@mui/system";
import { forwardRef, useEffect, useState } from "react";
import { AnagramInputFields } from "..";
import AnagramDisplayField from "../anagram-display-field";
import { useGamePlay } from "../../../../../contexts/game-play-context";
import * as dayjs from "dayjs";

const Alert = forwardRef(function Alert(props, ref) {
  return <MuiAlert elevation={6} variant="filled" ref={ref} {...props} />;
});

function AnagramHandler({ game }) {
  const { state, dispatch } = useGamePlay();

  const [isGuessCorrect, setIsGuessCorrect] = useState(null);
  const [open, setOpen] = useState(false);
  const [locallyStoredAttempts, setLocallyStoredAttempts] = useState({
    date: "",
    attempts: [],
  });
  const [activeAnagram, setActiveAnagram] = useState(
    game.gameAnagrams[state.activeAnagramIndex]
  );

  const displayAttempts = (anagram) => {
    let content = [];
    for (
      let attemptIndex = 0;
      attemptIndex < game.gameType.maxAttempts;
      attemptIndex++
    ) {
      anagram.isDisabled =
        anagram.gameUserGameAnagrams[0].attempts === attemptIndex &&
        !anagram.isSolved &&
        anagram.gameUserGameAnagrams[0].attempts <= game.gameType.maxAttempts
          ? false
          : true;
      content.push(
        <AnagramInputFields
          anagramId={anagram.id}
          key={`${anagram.id}-attempts-${attemptIndex}`}
          anagram={anagram.anagramWord}
          isDisabled={anagram.isDisabled}
          isAttempt={true}
          attempts={anagram.gameUserGameAnagrams[0].attempts}
          currentRow={attemptIndex}
          previousAttempt={
            locallyStoredAttempts.attempts
              ? locallyStoredAttempts.attempts[attemptIndex]
              : ""
          }
        ></AnagramInputFields>
      );
    }
    return content;
  };

  const updateAnagramAttempts = async () => {
    const response = await fetch(
      `http://localhost:5016/games/${game.id}/attempt/${state.updateAttempt.anagramId}`,
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          attempts: state.updateAttempt.attempts,
          attempt: state.updateAttempt.attempt,
        }),
      }
    );

    if (response.status === 200) {
      const data = await response.json();
      console.log(data);

      setIsGuessCorrect(data);
      console.log("success");

      dispatch({
        type: "incrementAnagramRow",
      });

      if (game.gameType.id == 1) {
        let arr = locallyStoredAttempts.attempts.map((x) => x);
        arr.push(state.updateAttempt.attempt);
        setLocallyStoredAttempts({
          date: dayjs().format("DD/MM/YYYY"),
          attempts: arr,
        });
      }
    } else {
      console.log("error");
    }
  };

  function submitAttempt() {
    updateAnagramAttempts();
  }

  function showToast() {
    if (isGuessCorrect != null) setOpen(true);
  }

  useEffect(() => {
    showToast();
  }, [isGuessCorrect]);

  useEffect(() => {
    setActiveAnagram(game.gameAnagrams[state.activeAnagramIndex]);
  }, [game]);

  useEffect(() => {
    if (game.gameType.id == 1) {
      const attempts = JSON.parse(
        localStorage.getItem("locallyStoredAttempts") || null
      );
      if (attempts && attempts.date == dayjs().format("DD/MM/YYYY")) {
        setLocallyStoredAttempts({
          date: attempts.date,
          attempts: attempts.attempts.map((x) => x),
        });
      } else {
        let newLocalEntry = {
          date: dayjs().format("DD/MM/YYYY"),
          attempts: [],
        };
        setLocallyStoredAttempts(newLocalEntry);
      }
    }
  }, []);

  useEffect(() => {
    if (game.gameType.id == 1) {
      if (locallyStoredAttempts.date != "") {
        localStorage.setItem(
          "locallyStoredAttempts",
          JSON.stringify(locallyStoredAttempts)
        );
      }
      console.log(locallyStoredAttempts);
    }
  }, [locallyStoredAttempts]);

  useEffect(() => {
    setActiveAnagram(game.gameAnagrams[state.activeAnagramIndex]);
  }, [state.activeAnagramIndex]);

  const handleClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }

    setOpen(false);
  };

  return (
    <>
      <Box key={`round${state.activeAnagramIndex}-anagram${activeAnagram.id}`}>
        <Box sx={{ mt: 5 }}>
          <Typography variant="h5" sx={{ mb: 0 }}>
            Challenge
          </Typography>
          <AnagramDisplayField
            anagram={activeAnagram.anagramWord}
          ></AnagramDisplayField>
        </Box>

        <Box sx={{ mt: 5 }}>
          <Typography variant="h5" sx={{ mb: 0 }}>
            Attempts
          </Typography>

          {displayAttempts(activeAnagram)}
        </Box>
      </Box>

      <Box sx={{ width: 1 / 4, m: "auto", mt: 5 }}>
        <Stack>
          <Button
            onClick={() => submitAttempt()}
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

      <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert
          onClose={handleClose}
          severity={isGuessCorrect ? "success" : "error"}
          sx={{ width: "100%" }}
        >
          {isGuessCorrect ? "Congratulations, anagram solved!" : "Try again!"}
        </Alert>
      </Snackbar>

      {/* {game.gameAnagrams.map((anagram, i) => (
        <Box
          key={`round${i}-anagram${anagram.id}`}
          hidden={state.activeAnagramIndex != i}
        >
          <Box sx={{ mt: 5 }}>
            <Typography variant="h5" sx={{ mb: 0 }}>
              Challenge
            </Typography>
            <AnagramDisplayField
              anagram={anagram.anagramWord}
            ></AnagramDisplayField>
          </Box>

          <Box sx={{ mt: 5 }}>
            <Typography variant="h5" sx={{ mb: 0 }}>
              Attempts
            </Typography>

            {displayAttempts(anagram, i)}
          </Box>
        </Box>
      ))}

      <Box sx={{ width: 1 / 4, m: "auto", mt: 5 }}>
        <Stack>
          <Button
            onClick={() => submitAttempt()}
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

      <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert
          onClose={handleClose}
          severity={isGuessCorrect ? "success" : "error"}
          sx={{ width: "100%" }}
        >
          {isGuessCorrect ? "Congratulations, anagram solved!" : "Try again!"}
        </Alert>
      </Snackbar> */}
    </>
  );
}

export default AnagramHandler;
