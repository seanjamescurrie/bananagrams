import { Slider, TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect } from "react";
import { useCreateGame } from "../../../../../contexts/create-game-context";

function DefineRules() {
  const game = useCreateGame();
  console.log(JSON.stringify(game));

  // const game = useContext(CreateGameContext);
  // const [title, setTitle] = useState(5);
  // const [totalAnagrams, setTotalAnagrams] = useState(5);
  // const [totalAttempts, setTotalAttempts] = useState(5);

  function updateGame(input) {
    game.dispatch({ type: input.action, payload: { value: input.value } });
  }

  // function updateTitle(value) {
  //   let updateGame = {
  //     userIds: game.createGame.userIds,
  //     title: value,
  //     totalAnagrams: game.createGame.totalAnagrams,
  //     totalAttempts: game.createGame.totalAttempts,
  //   };
  //   game.setCreateGame(updateGame);
  //   console.log(game.createGame.title);
  // }

  // function updateAnagrams(value) {
  //   let updateGame = {
  //     userIds: game.createGame.userIds,
  //     title: game.createGame.title,
  //     totalAnagrams: value,
  //     totalAttempts: game.createGame.totalAttempts,
  //   };
  //   game.setCreateGame(updateGame);
  //   console.log(game.createGame.totalAnagrams);
  // }

  // function updateAttempts(value) {
  //   let updateGame = {
  //     userIds: game.createGame.userIds,
  //     title: game.createGame.title,
  //     totalAnagrams: game.createGame.totalAnagrams,
  //     totalAttempts: value,
  //   };
  //   game.setCreateGame(updateGame);
  //   console.log(game.createGame.totalAttempts);
  // }

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
        {/* <Box sx={{ mt: 5 }}>
          <Typography variant="body1">
            Number of attempts allowed: {game.state.totalAttempts}
          </Typography>
          <Slider
            aria-label=""
            defaultValue={game.state.totalAttempts}
            //getAriaValueText
            valueLabelDisplay="auto"
            step={1}
            marks
            min={1}
            max={10}
            onChange={(e) => {
              updateGame({ action: "totalAttempts", value: e.target.value });
            }}
          />
        </Box> */}
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
