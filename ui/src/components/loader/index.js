import * as React from "react";
import { Box, CircularProgress } from "@mui/material";

function Loader() {
  return (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        justifyContent: "space-around",
        height: "80vh",
      }}
    >
      <CircularProgress />
    </Box>
  );
}

export default Loader;
