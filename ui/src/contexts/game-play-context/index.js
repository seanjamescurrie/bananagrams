import { createContext, useContext, useReducer } from "react";

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
    default: {
      throw new Error(`Unhandled action type: ${action.type}`);
    }
  }
  return state;
}

function GamePlayProvider({ children }) {
  const [state, dispatch] = useReducer(gamePlayReducer, {
    anagramRow: 0,
    updateAttempt: {
      attempt: "",
      anagramId: 0,
      attempts: 0,
    },
    activeAnagramIndex: 0,
  });

  const value = { state, dispatch };

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
