import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";

const darkTheme = createTheme({
  palette: {
    mode: "dark",
    primary: {
      main: "#fff59d",
    },
    secondary: {
      main: "#ff4081",
    },
    info: {
      main: "#90caf9",
    },
    success: {
      main: "#69f0ae",
    },
  },
  typography: {
    h1: {
      letterSpacing: 1,
      fontFamily: "Luckiest Guy",
    },
    h2: {
      letterSpacing: 1,
      fontFamily: "Luckiest Guy",
    },
    h3: {
      letterSpacing: 1,
      fontFamily: "Luckiest Guy",
    },
    h4: {
      letterSpacing: 1,
      fontFamily: "Luckiest Guy",
    },
    h5: {
      fontFamily: "Architects Daughter",
    },
    p: {
      fontSize: "1rem",
      fontFamily: "Architects Daughter",
    },
    body1: {
      fontFamily: "Roboto",
    },
    body2: {
      fontFamily: "Roboto",
    },
    button: {
      fontFamily: "Roboto",
    },
    label: {
      fontFamily: "Architects Daughter",
    },
  },
});

const lightTheme = createTheme({
  palette: {
    mode: "light",
    primary: {
      main: "#6a1b9a",
      light: "rgba(13,135,234,0.11)",
    },
    secondary: {
      main: "#f50057",
    },
    background: {
      default: "#fffde7",
    },
  },
  typography: {
    h1: {
      fontFamily: "Luckiest Guy",
    },
    h2: {
      fontFamily: "Luckiest Guy",
    },
    h3: {
      fontFamily: "Luckiest Guy",
    },
    h4: {
      fontFamily: "Luckiest Guy",
    },
    h5: {
      fontFamily: "Architects Daughter",
    },
    p: {
      fontSize: "1rem",
      fontFamily: "Architects Daughter",
    },
    body1: {
      fontFamily: "Roboto",
    },
    body2: {
      fontFamily: "Roboto",
    },
    button: {
      fontFamily: "Roboto",
    },
    label: {
      fontFamily: "Architects Daughter",
    },
  },
});

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <ThemeProvider theme={darkTheme}>
      <CssBaseline />
      <App />
    </ThemeProvider>
  </React.StrictMode>
);
