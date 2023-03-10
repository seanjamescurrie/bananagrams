import { createContext, useContext, useReducer } from "react";

const GamePlayContext = createContext();

function gamePlayReducer(state, action) {
  switch (action.type) {
    case "anagramRow": {
      return {
        ...state,
        anagramRow: action.payload.value,
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
