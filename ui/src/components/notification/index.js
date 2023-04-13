// import { Alert } from "@mui/material";
// import Snackbar from "@mui/material/Snackbar";
// import { useState } from "react";

// const Notification = ({ message, display }) => {
//   const [open, setOpen] = useState(display);

//   const handleClose = (event, reason) => {
//     if (reason === "clickaway") {
//       return;
//     }

//     setOpen(false);
//   };

//   return (
//     <>
//       <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
//         <Alert onClose={handleClose} severity="info" sx={{ width: "100%" }}>
//           {message}
//         </Alert>
//       </Snackbar>
//     </>
//   );
// };

import * as React from "react";
import Button from "@mui/material/Button";
import { SnackbarProvider, useSnackbar } from "notistack";

function Fragment({ message, variant, newNotificationCount }) {
  const { enqueueSnackbar } = useSnackbar();
  const [notificationCount, setNotificationCount] = React.useState(0);

  if (newNotificationCount > notificationCount) {
    enqueueSnackbar(message, { variant });
    setNotificationCount(newNotificationCount);
  }
}

const Notification = ({ message, variant, newNotificationCount }) => {
  return (
    <SnackbarProvider maxSnack={3}>
      <Fragment
        message={message}
        variant={variant}
        newNotificationCount={newNotificationCount}
      />
    </SnackbarProvider>
  );
};

export default Notification;
