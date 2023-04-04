import { Alert } from "@mui/material";
import Snackbar from "@mui/material/Snackbar";
import { useState } from "react";

const Notification = ({ message, display }) => {
  const [open, setOpen] = useState(display);

  const handleClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }

    setOpen(false);
  };

  return (
    <>
      <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert onClose={handleClose} severity="info" sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>
    </>
  );
};

export default Notification;
