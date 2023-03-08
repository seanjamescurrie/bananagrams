import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home, Login, SignUp, Game, CreateGame, Lobby, Games } from "./pages";
import { Navigation } from "./components";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useMemo, useState } from "react";
import { ThemeContext, themes } from "./contexts/theme-context";

function App() {
  const [mode, setMode] = useState(themes[0].theme);
  const colorMode = useMemo(
    () => ({
      toggleColorMode: () => {
        setMode((prevMode) =>
          prevMode === themes[0].theme ? themes[1].theme : themes[0].theme
        );
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
