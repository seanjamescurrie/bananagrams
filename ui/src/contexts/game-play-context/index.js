import { HubConnectionBuilder } from "@microsoft/signalr";
import {
  createContext,
  useContext,
  useEffect,
  useReducer,
  useState,
} from "react";

const GamePlayContext = createContext();

function gamePlayReducer(state, action) {
  switch (action.type) {
    case "incrementAnagramRow": {
      return {
        ...state,
        anagramRow: state.anagramRow + 1,
      };
    }
    case "updateAttempt": {
      return {
        ...state,
        updateAttempt: {
          attempt: action.payload.value.attempt,
          anagramId: action.payload.value.anagramId,
          attempts: action.payload.value.attempts,
        },
      };
    }
    case "incrementActiveAnagramIndex": {
      return {
        ...state,
        activeAnagramIndex: state.activeAnagramIndex + 1,
      };
    }
    case "startGame": {
      return {
        ...state,
        startMultiplayerGameId: action.payload.value,
      };
    }
    case "initialiseGameUsers": {
      return {
        ...state,
        users: action.payload.value.map((user) => ({
          id: user.id,
          username: user.username,
        })),
      };
    }
    case "updateMultiplayerUserAttempts": {
      let i = state.users.findIndex(
        (user) => user.id === action.payload.value.id
      );
      state.users[i].attempts = action.payload.value.attempts;
      state.users[i].isSolved = action.payload.value.isSolved;
      return {
        ...state,
      };
    }
    case "resetContext": {
      return {
        ...state,
        anagramRow: 0,
        updateAttempt: {
          attempt: "",
          anagramId: 0,
          attempts: 0,
        },
        activeAnagramIndex: 0,
        startMultiplayerGameId: 0,
        users: [],
      };
    }
    default: {
      throw new Error(`Unhandled action type: ${action.type}`);
    }
  }
  return state;
}

function GamePlayProvider({ children }) {
  const [connection, setConnection] = useState();
  const [gamePlayState, gamePlayDispatch] = useReducer(gamePlayReducer, {
    anagramRow: 0,
    updateAttempt: {
      attempt: "",
      anagramId: 0,
      attempts: 0,
    },
    activeAnagramIndex: 0,
    startMultiplayerGameId: 0,
    users: [],
  });

  // useEffect(() => {
  //   const connect = new HubConnectionBuilder()
  //     .withUrl("http://localhost:5016/hub/game")
  //     .withAutomaticReconnect()
  //     .build();

  //   setConnection(connect);

  //   return () => {
  //     connect.stop();
  //   };
  // }, []);

  // useEffect(() => {
  //   if (connection) {
  //     connection
  //       .start()
  //       .then(() => {
  //         // connection.on("StartGame", (gameId) => {
  //         //   gamePlayDispatch({
  //         //     type: "startGame",
  //         //     payload: {
  //         //       value: gameId,
  //         //     },
  //         //   });
  //         // });
  //         connection.on("SendUpdate", (updateUserAttempt) => {
  //           console.log(updateUserAttempt);
  //           // gamePlayDispatch({
  //           //   type: "updateMultiplayerUserAttempts",
  //           //   payload: {
  //           //     value: updateUserAttempt,
  //           //   },
  //           // });
  //         });
  //       })
  //       .catch((error) => console.log(error));
  //   }
  // }, [connection]);

  const value = { gamePlayState, gamePlayDispatch };

  return (
    <GamePlayContext.Provider value={value}>
      {children}
    </GamePlayContext.Provider>
  );
}

function useGamePlay() {
  const context = useContext(GamePlayContext);

  if (context === undefined) {
    throw new Error("useGamePlay must be used within a GamePlayProvider");
  }

  return context;
}

export { GamePlayProvider, useGamePlay };
