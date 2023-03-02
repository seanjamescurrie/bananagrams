import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home, Login, SignUp, Game, CreateGame, Lobby, Games } from "./pages";
import { Navigation } from "./components";

function App() {
  return (
    <BrowserRouter>
      <Navigation>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<SignUp />} />
          <Route path="/games" element={<Games />} />
          <Route path="/games/1" element={<Game />} />
          <Route path="/games" element={<CreateGame type="create" />} />
          <Route path="/games/1/lobby" element={<Lobby />} />
        </Routes>
      </Navigation>
    </BrowserRouter>
  );
}

export default App;
