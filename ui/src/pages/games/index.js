import { OpenLobbies, CompletedGames } from "./components";

const Games = () => {
  const viewCompleted = true;
  return (
    <>
      {viewCompleted ? (
        <CompletedGames></CompletedGames>
      ) : (
        <OpenLobbies></OpenLobbies>
      )}
    </>
  );
};

export default Games;
