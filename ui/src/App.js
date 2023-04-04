import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import {
  Home,
  Login,
  SignUp,
  Game,
  CreateGame,
  Lobby,
  Games,
  Results,
} from "./pages";
import { Navigation } from "./components";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useMemo, useState } from "react";
import { ThemeContext, themes } from "./contexts/theme-context";
import { CreateGameProvider } from "./contexts/create-game-context";
import { GamePlayProvider } from "./contexts/game-play-context";
import { NotificationProvider } from "./contexts/notification-context";
import { AuthContext } from "./contexts/";
import { LoginUtils } from "./utils";

const authenticatedRoutes = () => {
  return (
    <>
      <Route path="/" element={<Home />} />
      <Route path="/games" element={<Games />} />
      <Route path="/games/:id" element={<Game />} />
      <Route path="/games/create" element={<CreateGame type="create" />} />
      <Route path="/games/:id/lobby/" element={<Lobby />} />
      <Route path="/games/:id/results/" element={<Results />} />
      <Route path="*" element={<Navigate to="/" />} />
    </>
  );
};

const unAuthenticatedRoutes = () => {
  return (
    <>
      <Route path="/login" element={<Login />} />
      <Route path="/signup" element={<SignUp />} />
      <Route path="*" element={<Navigate to="/login" />} />
    </>
  );
};

function App() {
  const { authState } = AuthContext.useLogin();
  const [mode, setMode] = useState(themes[0].theme);

  const loggedIn =
    authState.accessToken && !LoginUtils.isTokenExpired(authState);

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
          <NotificationProvider>
            <Navigation>
              <CreateGameProvider>
                <GamePlayProvider>
                  <Routes>
                    {!loggedIn && unAuthenticatedRoutes()}
                    {loggedIn && authenticatedRoutes()}
                  </Routes>
                </GamePlayProvider>
              </CreateGameProvider>
            </Navigation>
          </NotificationProvider>
        </BrowserRouter>
      </ThemeProvider>
    </ThemeContext.Provider>
  );
}

export default App;
