import { createContext, useContext, useReducer } from "react";

const CreateGameContext = createContext();

function createGameReducer(state, action) {
  switch (action.type) {
    case "userIds": {
      return {
        ...state,
        userIds: action.payload.value,
      };
    }
    case "title":
      return {
        ...state,
        title: action.payload.value,
      };
    case "totalAnagrams":
      return {
        ...state,
        totalAnagrams: action.payload.value,
      };
    case "totalAttempts":
      return {
        ...state,
        totalAttempts: action.payload.value,
      };
    default: {
      throw new Error(`Unhandled action type: ${action.type}`);
    }
  }
  return state;
}

function CreateGameProvider({ children }) {
  const [state, dispatch] = useReducer(createGameReducer, {
    userIds: [],
    title: "",
    totalAnagrams: 5,
    totalAttempts: 3,
    gameAnagramTypeId: 2,
  });

  const value = { state, dispatch };
  return (
    <CreateGameContext.Provider value={value}>
      {children}
    </CreateGameContext.Provider>
  );
}

function useCreateGame() {
  const context = useContext(CreateGameContext);

  if (context === undefined) {
    throw new Error("useCreateGame must be used within a CreateGameProvider");
  }

  return context;
}

export { CreateGameProvider, useCreateGame };
