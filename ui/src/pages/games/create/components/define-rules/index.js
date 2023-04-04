import { Slider, TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect } from "react";
import { useCreateGame } from "../../../../../contexts/create-game-context";

function DefineRules() {
  const game = useCreateGame();

  function updateGame(input) {
    game.dispatch({ type: input.action, payload: { value: input.value } });
  }

  useEffect(() => {
    console.log(JSON.stringify(game));
  }, [game]);

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
          defaultValue={game.state.title}
          type="text"
          onChange={(e) => {
            updateGame({ action: "title", value: e.target.value });
          }}
        />
        <Box sx={{ mt: 5 }}>
          <Typography variant="body1">
            Number of anagrams: {game.state.totalAnagrams}
          </Typography>
          <Slider
            aria-label=""
            defaultValue={game.state.totalAnagrams}
            //getAriaValueText
            valueLabelDisplay="auto"
            step={1}
            marks
            min={1}
            max={10}
            onChange={(e) => {
              updateGame({ action: "totalAnagrams", value: e.target.value });
            }}
          />
        </Box>
      </Box>
    </>
  );
}

export default DefineRules;
