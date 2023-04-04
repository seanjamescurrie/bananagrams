import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { AuthContext } from "./contexts";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <AuthContext.AuthProvider>
      <App />
    </AuthContext.AuthProvider>
  </React.StrictMode>
);
