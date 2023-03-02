import { Grid, Slider, TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useState } from "react";

function DefineRules() {
  const [title, setTitle] = useState(5);
  const [totalAnagrams, setTotalAnagrams] = useState(5);
  const [totalAttempts, setTotalAttempts] = useState(5);

  return (
    <>
      <Typography variant="h5" sx={{ mt: 5 }}>
        Step 2
      </Typography>

      <Typography variant="p" color={"text.secondary"}>
        Define the rules of the game
      </Typography>

      <Box sx={{ width: 1 / 2, m: "auto", mt: 5 }}>
        <TextField
          fullWidth
          id="outlined-required"
          label="Title"
          defaultValue=""
          type="text"
          onChange={(e) => {
            setTitle(e.target.value);
          }}
        />
        <Box sx={{ mt: 5 }}>
          <Typography variant="body1">
            Number of anagrams: {totalAnagrams}
          </Typography>
          <Slider
            aria-label=""
            defaultValue={5}
            //getAriaValueText
            valueLabelDisplay="auto"
            step={1}
            marks
            min={1}
            max={10}
            onChange={(e) => {
              setTotalAnagrams(e.target.value);
            }}
          />
        </Box>
        <Box sx={{ mt: 5 }}>
          <Typography variant="body1">
            Number of attempts allowed: {totalAttempts}
          </Typography>
          <Slider
            aria-label=""
            defaultValue={5}
            //getAriaValueText
            valueLabelDisplay="auto"
            step={1}
            marks
            min={1}
            max={10}
            onChange={(e) => {
              setTotalAttempts(e.target.value);
            }}
          />
        </Box>
      </Box>

      {/* <Box
        component="form"
        sx={{
          mt: 5,
          "& .MuiTextField-root": { mb: 2 },
          textAlign: "center",
        }}
        noValidate
        autoComplete="off"
      >
        <Box sx={{ width: 1 / 2, ml: "auto" }}>
          <TextField
            fullWidth
            id="outlined-required"
            label="Search Users"
            defaultValue=""
            onChange={(e) => {
              setSearchUser(e.target.value);
            }}
          />
        </Box>
      </Box> */}
    </>
  );
}

export default DefineRules;
