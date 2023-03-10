import { useTheme } from "@emotion/react";
import { Grid, TextField } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import { AnagramAttemptContext } from "../../../../../contexts/game-context";

function splitAnagram(anagram, isAttempt) {
  let arr = [];
  for (let i = 0; i < anagram.length; i++) {
    let char = "";
    if (!isAttempt) char = anagram.charAt(i);
    arr.push(char);
  }
  return arr;
}

function AnagramInputFields({
  anagram,
  isDisabled,
  isAttempt,
  anagramId,
  attempts,
  currentRow,
}) {
  const maxInputLength = 1;

  const attempt = useContext(AnagramAttemptContext);

  const [anagramArray, setAnagramArray] = useState([]);

  useEffect(() => {
    setAnagramArray(splitAnagram(anagram, isAttempt));
  }, []);

  function changeHandler(value, i) {
    let updatedAttempt = {
      attempt: "",
      anagramId: anagramId,
      attempts: attempts,
    };

    anagramArray[i] = value;
    setAnagramArray(anagramArray);
    for (let letter of anagramArray) {
      updatedAttempt.attempt += letter;
    }
    attempt.setAnagramAttempt(updatedAttempt);

    if (value.length == maxInputLength) {
      const nextSibiling = document.getElementById(
        `${anagram}-row${currentRow}-char${i + 1}`
      );
      console.log(nextSibiling);
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
