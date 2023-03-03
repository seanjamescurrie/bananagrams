import { useEffect, useState } from "react";
import { OpenLobbies, CompletedGames } from "./components";

const Games = () => {
  const viewCompleted = true;
  const [games, setGames] = useState([]);

  async function fetchData() {
    const response = await fetch("http://localhost:5016/games", {
      method: "GET",
    });
    if (response.status === 200) {
      const data = await response.json();
      let foundGames = data.map((game) => ({
        id: game.id,
        title: game.title,
        dateCreated: game.dateCreated,
        users: game.gameUsers.map((user) => ({
          username: user.username,
        })),
      }));
      setGames(foundGames);
    }
  }

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <>
      {viewCompleted ? (
        <CompletedGames></CompletedGames>
      ) : (
        <OpenLobbies games={games}></OpenLobbies>
      )}
    </>
  );
};

export default Games;
