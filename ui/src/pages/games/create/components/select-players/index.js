import { TextField, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { DataGrid } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { useCreateGame } from "../../../../../contexts/create-game-context";

function SelectPlayers() {
  const game = useCreateGame();

  const [rows, setRows] = useState([]);
  const [users, setUsers] = useState([]);
  const [searchUser, setSearchUser] = useState("");
  console.log(JSON.stringify(game));

  const columns = [
    { field: "id", headerName: "ID", flex: 0.5 },
    {
      field: "firstName",
      headerName: "First name",
      flex: 1,
      editable: true,
    },
    {
      field: "lastName",
      headerName: "Last name",
      flex: 1,
      editable: true,
    },
    {
      field: "username",
      headerName: "Username",
      sortable: false,
      flex: 2,
    },
  ];

  const getUsers = async () => {
    const response = await fetch("http://localhost:5016/users", {
      method: "GET",
    });
    if (response.status === 200) {
      const data = await response.json();
      let foundUsers = data.map((user) => ({
        id: user.id,
        firstName: user.firstName,
        lastName: user.lastName,
        username: user.username,
      }));
      setRows(foundUsers);
      setUsers(foundUsers);
    } else {
      console.log("error");
    }
  };

  async function updateUserList() {
    let searchResults = [];
    if (searchUser !== "") {
      users.forEach((user) => {
        if (
          (user.firstName !== null &&
            user.firstName.toLowerCase().includes(searchUser)) ||
          (user.lastName !== null &&
            user.lastName.toLowerCase().includes(searchUser))
        ) {
          searchResults.push(user);
        }
      });

      setRows(searchResults);
    } else {
      setRows(users);
    }
  }

  useEffect(() => {
    updateUserList();
  }, [searchUser]);

  useEffect(() => {
    getUsers();
  }, []);

  function updateUserIdList(userIds) {
    game.dispatch({ type: "userIds", payload: { value: userIds } });
  }

  return (
    <>
      <Typography variant="h5" sx={{ mt: 5 }}>
        Step 1
      </Typography>

      <Typography variant="p" color={"text.secondary"}>
        Search for users to challenge
      </Typography>

      <Box
        component="form"
        sx={{
          mt: 5,
          "& .MuiTextField-root": { mb: 2 },
          textAlign: "center",
        }}
        noValidate
        autoComplete="off"
      >
        <Box sx={{ width: 1 / 2, m: "auto" }}>
          <TextField
            fullWidth
            id="outlined-required"
            label="Search Users"
            defaultValue=""
            onChange={(e) => {
              setSearchUser(e.target.value.toLowerCase());
            }}
          />
        </Box>

        <Box sx={{ height: 400, width: "100%" }}>
          <DataGrid
            rows={rows}
            columns={columns}
            pageSize={5}
            rowsPerPageOptions={[5]}
            checkboxSelection
            disableSelectionOnClick
            experimentalFeatures={{ newEditingApi: true }}
            selectionModel={game.state.userIds}
            onSelectionModelChange={(userIds) => updateUserIdList(userIds)}
          />
        </Box>
      </Box>
    </>
  );
}

export default SelectPlayers;
