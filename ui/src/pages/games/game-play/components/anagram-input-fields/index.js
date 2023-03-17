import { useTheme } from "@emotion/react";
import { Grid, TextField } from "@mui/material";
import { red } from "@mui/material/colors";
import { useEffect, useState } from "react";
import { useGamePlay } from "../../../../../contexts/game-play-context";

function splitAnagram(anagram, previousAttempt) {
  let arr = [];
  for (let i = 0; i < anagram.length; i++) {
    let char = "";
    if (previousAttempt != null && previousAttempt != "")
      char = previousAttempt.charAt(i);
    arr.push(char);
  }
  return arr;
}

function AnagramInputFields({
  anagram,
  isDisabled,
  anagramId,
  attempts,
  currentRow,
  previousAttempt,
}) {
  const maxInputLength = 1;

  const { dispatch } = useGamePlay();

  const [anagramArray, setAnagramArray] = useState([]);

  useEffect(() => {
    setAnagramArray(splitAnagram(anagram, previousAttempt));
  }, [previousAttempt]);

  function changeHandler(value, i) {
    let attempt = "";

    anagramArray[i] = value;
    setAnagramArray(anagramArray);
    for (let letter of anagramArray) {
      attempt += letter;
    }
    dispatch({
      type: "updateAttempt",
      payload: {
        value: {
          attempts: attempts + 1,
          anagramId: anagramId,
          attempt: attempt,
        },
      },
    });

    if (value.length == maxInputLength) {
      const nextSibiling = document.getElementById(
        `${anagram}-row${currentRow}-char${i + 1}`
      );
      if (nextSibiling !== null) {
        nextSibiling.focus();
      }
    }
  }

  return (
    <Grid
      container
      spacing={2}
      justifyContent="center"
      columns={14}
      sx={{ mt: 1 }}
    >
      {anagramArray.map((letter, i) => (
        <Grid item xs={1} key={`${anagram}-attempt-input-${i}`}>
          <TextField
            id={`${anagram}-row${currentRow}-char${i}`}
            variant="outlined"
            placeholder={letter}
            sx={{
              width: "3.5em",
              height: "3.5em",
              textAlign: "center",
            }}
            disabled={isDisabled}
            inputProps={{
              maxLength: maxInputLength,
              style: { textTransform: "uppercase" },
            }}
            onChange={(e) => {
              changeHandler(e.target.value, i);
            }}
          />
        </Grid>
      ))}
    </Grid>
  );
}

export default AnagramInputFields;
