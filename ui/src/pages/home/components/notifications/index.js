import { HubConnectionBuilder } from "@microsoft/signalr";
import { Box, Button, TextField } from "@mui/material";
import React, { useEffect, useState } from "react";

export const Notify = () => {
  const [connection, setConnection] = useState(null);
  const [inputText, setInputText] = useState("");

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl("http://localhost:5016/hub")
      .withAutomaticReconnect()
      .build();

    setConnection(connect);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("connected");
          connection.on("ReceiveMessage", (message) => {
            // notification.open({
            //   message: "New Notification",
            //   description: message,
            // });
            console.log(message);
          });
        })
        .catch((error) => console.log(error));
    }
  }, [connection]);

  const sendMessage = async () => {
    const notification = {
      user: "username",
      message: inputText,
    };
    if (connection) await connection.send("SendMessage", notification);
    setInputText("");
  };

  return (
    <Box sx={{ display: "flex", justifyContent: "center", mt: 5 }}>
      <TextField
        value={inputText}
        onChange={(input) => {
          setInputText(input.target.value);
        }}
        sx={{ mr: 2 }}
      />
      <Button variant="contained" onClick={sendMessage} type="primary">
        Send
      </Button>
    </Box>
  );
};
