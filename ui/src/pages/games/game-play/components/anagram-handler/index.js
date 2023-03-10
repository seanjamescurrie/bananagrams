import { Button, Stack, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useContext, useEffect, useMemo, useState } from "react";
import { AnagramInputFields } from "..";
import AnagramDisplayField from "../anagram-display-field";
import { AnagramAttemptContext } from "../../../../../contexts/game-context";
import { useGamePlay } from "../../../../../contexts/game-play-context";

function AnagramHandler({ game }) {
  const gamePlayContext = useGamePlay();

  const [anagramAttempt, setAnagramAttempt] = useState({
    attempt: "",
    anagramId: 0,
    attempts: 0,
  });
  const attempt = useMemo(
    () => ({ anagramAttempt, setAnagramAttempt }),
    [anagramAttempt]
  );

  const displayAttempts = (anagram) => {
    let content = [];
    for (let i = 0; i < game.gameType.maxAttempts; i++) {
      anagram.isDisabled =
        anagram.gameUserGameAnagrams[0].attempts == i &&
        !anagram.isSolved &&
        anagram.gameUserGameAnagrams[0].attempts <= game.gameType.maxAttempts
          ? false
          : true;
      content.push(
        <AnagramAttemptContext.Provider
          value={attempt}
          key={`${anagram.id}-context-${i}`}
        >
          <AnagramInputFields
            anagramId={anagram.id}
            key={`${anagram.id}-attempts-${i}`}
            anagram={anagram.anagramWord}
            isDisabled={anagram.isDisabled}
            isAttempt={true}
            attempts={anagram.gameUserGameAnagrams[0].attempts}
            currentRow={i}
          ></AnagramInputFields>
        </AnagramAttemptContext.Provider>
      );
    }
    return content;
  };

  const updateAnagramAttempts = async () => {
    const response = await fetch(
      `http://localhost:5016/games/${game.id}/attempt/${anagramAttempt.anagramId}`,
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          attempts: anagramAttempt.attempts,
          attempt: anagramAttempt.attempt,
        }),
      }
    );
    if (response.status === 200) {
      const data = await response.json();
      console.log(data);
      console.log("success");
    } else {
      console.log("error");
    }
  };

  function submitAttempt() {
    anagramAttempt.attempts = anagramAttempt.attempts + 1;
    setAnagramAttempt(anagramAttempt);
    console.log(anagramAttempt);
    updateAnagramAttempts();
    gamePlayContext.dispatch({
      type: "anagramRow",
      payload: { value: row2.state.anagramRow + 1 },
    });
  }

  return (
    <>
      {game.gameAnagrams.map((anagram, i) => (
        <Box key={`${anagram.id}-${i}`}>
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

            {displayAttempts(anagram)}
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
    </>
  );
}

export default AnagramHandler;
