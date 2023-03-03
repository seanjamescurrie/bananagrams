import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home, Login, SignUp, Game, CreateGame, Lobby, Games } from "./pages";
import { Navigation } from "./components";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { createContext, useMemo, useState } from "react";
import { ThemeContext, themes } from "./contexts/theme-context";
import { useTheme } from "@emotion/react";

export const ColorModeContext = createContext({
  toggleColorMode: () => {},
});

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

function App() {
  const themee = useTheme();
  const [mode, setMode] = useState(themes[1].theme);
  const colorMode = useMemo(
    () => ({
      toggleColorMode: () => {
        setMode((prevMode) =>
          prevMode === themes[0].theme ? themes[1].theme : themes[0].theme
        );
        console.log(themee.palette.elements.pink);
      },
    }),
    []
  );

  const theme = useMemo(() => createTheme(mode), [mode]);

  return (
    <ThemeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <BrowserRouter>
          <Navigation>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/login" element={<Login />} />
              <Route path="/signup" element={<SignUp />} />
              <Route path="/games" element={<Games />} />
              <Route path="/games/1" element={<Game />} />
              <Route
                path="/games/create"
                element={<CreateGame type="create" />}
              />
              <Route path="/games/1/lobby" element={<Lobby />} />
            </Routes>
          </Navigation>
        </BrowserRouter>
      </ThemeProvider>
    </ThemeContext.Provider>
  );
}

export default App;
