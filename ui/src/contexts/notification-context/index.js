import { HubConnectionBuilder } from "@microsoft/signalr";
import {
  createContext,
  useContext,
  useEffect,
  useReducer,
  useState,
} from "react";

const NotificationContext = createContext();

function notificationReducer(state, action) {
  switch (action.type) {
    case "notificationCount": {
      return {
        ...state,
        totalNotifications: state.totalNotifications + 1,
      };
    }
  }
}

function NotificationProvider({ children }) {
  const [connection, setConnection] = useState();
  const baseUrl = process.env.REACT_APP_API_URL ?? "http://localhost:5016";

  const [state, dispatch] = useReducer(notificationReducer, {
    totalNotifications: 0,
  });

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl(`${baseUrl}/hub`)
      .withAutomaticReconnect()
      .build();

    setConnection(connect);

    return () => {
      connect.stop();
    };
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          connection.on("NotificationCount", () => {
            dispatch({
              type: "notificationCount",
            });
          });
        })
        .catch((error) => console.log(error));
    }
  }, [connection]);

  const value = { state, dispatch };

  return (
    <NotificationContext.Provider value={value}>
      {children}
    </NotificationContext.Provider>
  );
}

function useNotification() {
  const context = useContext(NotificationContext);

  if (context === undefined) {
    throw new Error("useNotification must be used within NotificationProvider");
  }

  return context;
}

export { NotificationProvider, useNotification };
