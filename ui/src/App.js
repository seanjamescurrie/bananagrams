import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home, Login, SignUp, Game, CreateGame, Lobby, Games } from "./pages";
import { Navigation } from "./components";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useMemo, useState } from "react";
import { ThemeContext, themes } from "./contexts/theme-context";

import { CreateGameProvider } from "./contexts/create-game-context";
import { GamePlayProvider } from "./contexts/game-play-context";

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
            <CreateGameProvider>
              <GamePlayProvider>
                <Routes>
                  <Route path="/" element={<Home />} />
                  <Route path="/login" element={<Login />} />
                  <Route path="/signup" element={<SignUp />} />
                  <Route path="/games" element={<Games />} />
                  <Route path="/games/:id" element={<Game />} />
                  <Route
                    path="/games/create"
                    element={<CreateGame type="create" />}
                  />
                  <Route path="/games/:id/lobby/" element={<Lobby />} />
                </Routes>
              </GamePlayProvider>
            </CreateGameProvider>
          </Navigation>
        </BrowserRouter>
      </ThemeProvider>
    </ThemeContext.Provider>
  );
}

export default App;
