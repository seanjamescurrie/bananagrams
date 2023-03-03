import { createTheme } from "@mui/system";
import { createContext } from "react";

export const themes = [
  {
    name: "darkTheme",
    theme: createTheme({
      palette: {
        mode: "dark",
        primary: {
          main: "#fff59d",
        },
        secondary: {
          main: "#fff9c4",
          light: "#f8bbd0",
          dark: "#ffea00",

          contrastText: "#fff",
        },
        info: {
          main: "#90caf9",
        },
        success: {
          main: "#69f0ae",
        },
        elements: {
          pink: "LightPink",
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
    }),
  },
  {
    name: "lightTheme",
    theme: createTheme({
      palette: {
        mode: "light",
        primary: {
          main: "#6a1b9a",
          light: "rgba(13,135,234,0.11)",
        },
        secondary: {
          main: "#ff9800",
          light: "#e91e63",
          dark: "#ffea00",
          contrastText: "#fff",
        },
        background: {
          default: "#fffde7",
        },
        elements: {
          pink: "DeepPink",
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
    }),
  },
];

export const ThemeContext = createContext({
  setColorMode: () => {},
});
