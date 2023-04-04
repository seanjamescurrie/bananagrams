import React, { createContext, useContext, useReducer } from "react";
import { StorageService } from "../../services";

const AuthContext = createContext();

function authReducer(state, action) {
  switch (action.type) {
    case "authentication": {
      return {
        ...state,
        accessToken: action.accessToken,
        refreshToken: action.refreshToken,
      };
    }
    case "logout": {
      return {};
    }
    default: {
      throw new Error(`Unhandled action type: ${action.type}`);
    }
  }
}

function AuthProvider({ children }) {
  const auth = StorageService.getLocalStorage("auth") ?? {};
  const [authState, authDispatch] = useReducer(authReducer, auth);
  const value = { authState, authDispatch };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

function useLogin() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    console.log(context);
    throw new Error("useLogin must be used within a AuthProvider");
  }
  return context;
}
export default { AuthProvider, useLogin };
