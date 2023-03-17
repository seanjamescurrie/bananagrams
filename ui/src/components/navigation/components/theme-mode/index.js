import { useTheme } from "@emotion/react";
import { Box, IconButton } from "@mui/material";
import { useContext } from "react";
import Brightness4Icon from "@mui/icons-material/Brightness3";
import Brightness7Icon from "@mui/icons-material/Brightness7";
import { ThemeContext } from "../../../../contexts/theme-context";
import ArrowRightAltIcon from "@mui/icons-material/ArrowRightAlt";

function ThemeMode() {
  const theme = useTheme();
  const colorMode = useContext(ThemeContext);

  return (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        color: "custom.themeMode",
        borderRadius: 1,
        p: 3,
      }}
    >
      {theme.palette.mode === "dark" ? "light" : "dark"} mode
      <ArrowRightAltIcon sx={{ ml: 1 }} />
      <IconButton
        sx={{ ml: 1 }}
        onClick={colorMode.toggleColorMode}
        color="inherit"
      >
        {theme.palette.mode === "dark" ? (
          <Brightness7Icon />
        ) : (
          <Brightness4Icon />
        )}
      </IconButton>
    </Box>
  );
}

export default ThemeMode;
