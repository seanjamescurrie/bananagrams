import * as React from "react";
import PropTypes from "prop-types";
import LinearProgress from "@mui/material/LinearProgress";
import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import { useEffect, useState } from "react";
import { useGamePlay } from "../../../../../contexts/game-play-context";
import { useNavigate } from "react-router-dom";

function LinearProgressWithLabel(props) {
  return (
    <Box sx={{ display: "flex", alignItems: "center" }}>
      <Box sx={{ width: "100%", mr: 1 }}>
        <LinearProgress variant="determinate" {...props} />
      </Box>
      <Box sx={{ minWidth: 35 }}>
        <Typography variant="body2" color="text.secondary">
          00:{`0${Math.round(props.value / 3.333)}`.slice(-2)}
        </Typography>
      </Box>
    </Box>
  );
}

LinearProgressWithLabel.propTypes = {
  /**
   * The value of the progress indicator for the determinate and buffer variants.
   * Value between 0 and 100.
   */
  value: PropTypes.number.isRequired,
};

function Timer({ totalAnagrams, gameId }) {
  const { state, dispatch } = useGamePlay();
  const [timer, setTimer] = useState(100);
  const navigate = useNavigate();

  useEffect(() => {
    console.log(timer);
    timer > 0 && setTimeout(() => setTimer(timer - 3.333), 1000);
    if (state.activeAnagramIndex == totalAnagrams - 1 && timer <= 0) {
      navigate(`/games/${gameId}/results`);
    } else if (timer <= 0) {
      dispatch({ type: "incrementActiveAnagramIndex" });
      setTimer(100);
    }
  }, [timer]);

  return (
    <Box sx={{ width: "100%" }}>
      <LinearProgressWithLabel value={timer} />
    </Box>
  );
}

export default Timer;
