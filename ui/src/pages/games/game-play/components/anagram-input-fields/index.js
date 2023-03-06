import { useTheme } from "@emotion/react";
import { Grid, TextField } from "@mui/material";
import { useEffect, useState } from "react";

function splitAnagram(anagram, isAttempt) {
  let arr = [];
  for (let i = 0; i < anagram.length; i++) {
    let char = "";
    if (!isAttempt) char = anagram.charAt(i);
    arr.push(char);
  }
  return arr;
}

function AnagramInputFields({ anagram, isDisabled, isAttempt }) {
  const [anagramArray, setAnagramArray] = useState([]);

  useEffect(() => {
    setAnagramArray(splitAnagram(anagram, isAttempt));
  }, []);

  return (
    <Grid
      container
      spacing={2}
      justifyContent="center"
      columns={14}
      sx={{ mt: 1 }}
    >
      {anagramArray.map((letter) => (
        <Grid item xs={1}>
          <TextField
            id="outlined-basic"
            variant="outlined"
            placeholder={letter}
            sx={{
              width: "3.5em",
              height: "3.5em",
              textAlign: "center",
            }}
            disabled={isDisabled}
            inputProps={{ maxLength: 1, style: { textTransform: "uppercase" } }}
          />
        </Grid>
      ))}
    </Grid>
  );
}

export default AnagramInputFields;
