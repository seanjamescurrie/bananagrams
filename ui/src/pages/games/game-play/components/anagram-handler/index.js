import { Button, Snackbar, Stack, Typography } from "@mui/material";
import MuiAlert from "@mui/material/Alert";
import { Box } from "@mui/system";
import { forwardRef, useEffect, useState } from "react";
import { AnagramInputFields } from "..";
import AnagramDisplayField from "../anagram-display-field";
import { useGamePlay } from "../../../../../contexts/game-play-context";
import { AuthContext } from "../../../../../contexts";
import * as dayjs from "dayjs";
import { GameService } from "../../../../../services";
import { LoginUtils } from "../../../../../utils";
import { Notification } from "../../../../../components";

const Alert = forwardRef(function Alert(props, ref) {
  return <MuiAlert elevation={6} variant="filled" ref={ref} {...props} />;
});

function AnagramHandler({ game }) {
  const { gamePlayState, gamePlayDispatch } = useGamePlay();

  const [isGuessCorrect, setIsGuessCorrect] = useState(null);
  const [locallyStoredAttempts, setLocallyStoredAttempts] = useState({
    date: "",
    attempts: [],
  });
  const [activeAnagram, setActiveAnagram] = useState(
    game.gameAnagrams[gamePlayState.activeAnagramIndex]
  );
  const [notificationCount, setNotificationCount] = useState(1);
  const { authState } = AuthContext.useLogin();
  const currentUser = LoginUtils.getAccountId(authState.accessToken);

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
    const response = await GameService.updateAnagramAttempt(
      game.id,
      gamePlayState.updateAttempt
    );

    if (response.status === 200) {
      const data = await response.json();

      setIsGuessCorrect(data);
      console.log("success");

      gamePlayDispatch({
        type: "incrementAnagramRow",
      });

      setNotificationCount(notificationCount + 1);

      if (game.gameType.id == 1) {
        let arr = locallyStoredAttempts.attempts.map((x) => x);
        arr.push(gamePlayState.updateAttempt.attempt);
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

  useEffect(() => {
    setActiveAnagram(game.gameAnagrams[gamePlayState.activeAnagramIndex]);
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
    setActiveAnagram(game.gameAnagrams[gamePlayState.activeAnagramIndex]);
  }, [gamePlayState.activeAnagramIndex]);

  return (
    <>
      <Box
        key={`round${gamePlayState.activeAnagramIndex}-anagram${activeAnagram.id}`}
      >
        <Box sx={{ mt: 5 }}>
          <Typography variant="h5" sx={{ mb: 0 }}>
            Challenge
          </Typography>
          {activeAnagram.anagramWord && (
            <AnagramDisplayField
              anagram={activeAnagram.anagramWord}
            ></AnagramDisplayField>
          )}
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
            disabled={
              activeAnagram.gameUserGameAnagrams[0].attempts >=
              game.gameType.maxAttempts
            }
          >
            Submit
          </Button>
          {/* <Button
            variant="outlined"
            width="lg"
            sx={{
              mb: 2,
            }}
          >
            Clear
          </Button> */}
        </Stack>
      </Box>

      {isGuessCorrect !== null && (
        <Notification
          message={
            isGuessCorrect ? "Congratulations, anagram solved!" : "Try again!"
          }
          variant={isGuessCorrect ? "success" : "error"}
          newNotificationCount={notificationCount}
        ></Notification>
      )}
    </>
  );
}

export default AnagramHandler;
