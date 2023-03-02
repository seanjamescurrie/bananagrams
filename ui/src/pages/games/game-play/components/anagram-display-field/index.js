import { Box, Grid, Typography } from "@mui/material";
import { useEffect, useState } from "react";

function splitAnagram(anagram) {
  let arr = [];
  for (let i = 0; i < anagram.length; i++) {
    arr.push(anagram.charAt(i));
  }
  return arr;
}

function AnagramDisplayField({ anagram }) {
  const [anagramArray, setAnagramArray] = useState([]);

  useEffect(() => {
    setAnagramArray(splitAnagram(anagram));
  }, []);

  return (
    // <Grid sx={{ flexGrow: 1 }} container spacing={2}>
    //   <Grid item xs={12}>
    <Grid
      container
      spacing={2}
      justifyContent="center"
      columns={14}
      sx={{ mt: 1 }}
    >
      {anagramArray.map((letter) => (
        <Grid item xs={1}>
          <Box
            sx={{
              p: 2,
              border: "1px solid grey",
              borderRadius: 1,
              width: "3.5em",
              height: "3.5em",
              textAlign: "center",
            }}
          >
            <Typography
              id="outlined-basic"
              variant="outlined"
              value={letter}
              sx={{ width: "5em", textAlign: "center" }}
              disabled
              maxRows="1"
            >
              {letter}
            </Typography>
          </Box>
        </Grid>
      ))}
    </Grid>
    //   </Grid>
    // </Grid>
  );
}

export default AnagramDisplayField;
