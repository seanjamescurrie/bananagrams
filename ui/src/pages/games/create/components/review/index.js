import { Avatar, Divider, Grid, Typography } from "@mui/material";
import { Box, Stack } from "@mui/system";
import { useEffect, useState } from "react";
import { useCreateGame } from "../../../../../contexts/create-game-context";

function ReviewGame() {
  const game = useCreateGame();
  const [users, setUsers] = useState([]);

  const getUsers = async () => {
    const response = await fetch("http://localhost:5016/users", {
      method: "GET",
    });
    if (response.status === 200) {
      const data = await response.json();
      let foundUsers = data.map((user) => ({
        id: user.id,
        username: user.username,
      }));
      let filteredUsers = [];
      foundUsers.map((user) => {
        if (game.state.userIds.includes(user.id)) {
          filteredUsers.push(user);
        }
      });
      setUsers(filteredUsers);
      console.log(users);
    } else {
      console.log("error");
    }
  };

  useEffect(() => {
    getUsers();
  }, []);

  return (
    <>
      <Typography variant="h5" sx={{ mt: 5 }}>
        Step 3
      </Typography>
      <Typography variant="p" color={"text.secondary"}>
        Review game and send challenge to players
      </Typography>
      <Box textAlign="left" sx={{ m: "auto", mt: 5, width: 1 / 2 }}>
        <Box sx={{ mt: 5, mb: 2 }}>
          <Typography variant="h5"> Title</Typography>
          <Typography variant="h4"> {game.state.title}</Typography>
        </Box>
        <Divider />
        <Box sx={{ mt: 2, mb: 4 }}>
          <Typography variant="h5"> Users</Typography>
          <Stack direction="row">
            {users.map((user) => (
              <Box sx={{ m: "auto", mt: 1 }} key={`${user.username}`}>
                <Avatar sx={{ m: "auto", mb: 1 }}></Avatar>
                <Typography variant="body1">{user.username}</Typography>
              </Box>
            ))}
          </Stack>
        </Box>
        <Divider />
        <Grid container spacing={2} sx={{ mt: 2 }}>
          <Grid item xs={8}>
            <Typography variant="h5"> Number of anagrams</Typography>
          </Grid>
          <Grid item xs={4}>
            <Typography variant="h4" sx={{ textAlign: "right" }}>
              {game.state.totalAnagrams}
            </Typography>
          </Grid>
          <Grid item xs={8}>
            <Typography variant="h5"> Number of attempts allowed</Typography>
          </Grid>
          <Grid item xs={4}>
            <Typography variant="h4" sx={{ textAlign: "right" }}>
              {game.state.totalAttempts}
            </Typography>
          </Grid>
          <Grid item xs={12} sx={{ mb: 2 }}>
            <Divider />
          </Grid>

          <Grid item xs={8}>
            <Typography variant="h5" color={"text.secondary"}>
              Time limit
            </Typography>
          </Grid>
          <Grid item xs={4}>
            <Typography
              variant="h4"
              color={"text.secondary"}
              sx={{ textAlign: "right" }}
            >
              30 sec
            </Typography>
          </Grid>
        </Grid>
      </Box>
    </>
  );
}

export default ReviewGame;
