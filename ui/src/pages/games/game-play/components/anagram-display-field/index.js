import { Box, Grid, Typography } from "@mui/material";
import { useEffect, useState } from "react";

function splitAnagram(anagram) {
  let arr = [];
  for (let i = 0; i < anagram.length; i++) {
    arr.push(anagram.charAt(i));
  }

  console.log(arr);
  return arr;
}

function AnagramDisplayField({ anagram }) {
  const [anagramArray, setAnagramArray] = useState([]);

  useEffect(() => {
    console.log(anagram);
    setAnagramArray(splitAnagram(anagram.toUpperCase()));
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
      {anagramArray.map((letter, i) => (
        <Grid item xs={1} key={`${letter}-${i}`}>
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
