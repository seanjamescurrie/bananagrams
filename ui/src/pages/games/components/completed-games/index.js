import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import {
  Avatar,
  Card,
  CardActions,
  CardContent,
  Collapse,
  Divider,
  Grid,
  IconButton,
  Stack,
  Typography,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import { Box, Container } from "@mui/system";
import { useEffect, useState } from "react";
import * as dayjs from "dayjs";
import { useTheme } from "@emotion/react";

const ExpandMore = styled((props) => {
  const { expand, ...other } = props;
  return <IconButton {...other} />;
})(({ theme, expand }) => ({
  transform: !expand ? "rotate(0deg)" : "rotate(180deg)",
  marginLeft: "auto",
  transition: theme.transitions.create("transform", {
    duration: theme.transitions.duration.shortest,
  }),
}));

function CompletedGames() {
  const theme = useTheme();
  const [expanded, setExpanded] = useState(false);
  const [games, setGames] = useState([]);

  async function fetchData() {
    const response = await fetch("http://localhost:5016/games", {
      method: "GET",
    });
    if (response.status === 200) {
      const data = await response.json();
      let foundGames = data.map((game) => ({
        id: game.id,
        title: game.title,
        dateCreated: game.dateCreated,
        users: game.gameUsers.map((user) => ({
          username: user.username,
        })),
        expanded: false,
      }));
      setGames(foundGames);
    }
  }

  useEffect(() => {
    fetchData();
  }, []);

  const handleExpandClick = (id) => {
    console.log(games);
    let updated = games.map((game) => ({
      id: game.id,
      title: game.title,
      dateCreated: game.dateCreated,
      users: game.users.map((user) => ({
        username: user.username,
      })),
      expanded: game.id === id ? !game.expanded : game.expanded,
    }));

    setGames(updated);
  };

  return (
    <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
      {/* <Typography variant="h2">Completed Games</Typography>
      <Typography variant="p">Game details of completed games</Typography> */}

      {games.map((game) => (
        <Box alignItems="center" sx={{ mt: 5 }} key={game.id}>
          <Card
            sx={{
              m: "auto",
              border: 1,
              borderRadius: "30px",
              borderColor: theme.palette.text.main,
              p: 0,
            }}
          >
            <CardContent paddingbottom="0">
              <Grid container spacing={2} sx={{ p: 1 }}>
                <Grid
                  item
                  xs={6}
                  sx={{
                    textAlign: "left",
                  }}
                >
                  <Typography variant="p">Title</Typography>
                  <Typography variant="h4">{game.title}</Typography>
                </Grid>
                <Grid item xs={3} sx={{ textAlign: "left" }}>
                  <Typography variant="p">Total Anagrams</Typography>
                  <Typography variant="h4">3</Typography>
                </Grid>
                <Grid item xs={3} sx={{ textAlign: "left" }}>
                  <Typography variant="p">Date Created</Typography>
                  <Typography variant="h4">
                    {dayjs().format(game.dateCreated).substring(0, 10)}
                  </Typography>
                </Grid>
              </Grid>
            </CardContent>

            <Divider variant="middle" />
            <CardActions disableSpacing>
              <ExpandMore
                expand={game.expanded}
                onClick={() => handleExpandClick(game.id)}
                aria-expanded={expanded}
                aria-label="show more"
                sx={{ p: 0, mr: 2 }}
              >
                <ExpandMoreIcon />
              </ExpandMore>
            </CardActions>
            <Collapse in={game.expanded} timeout="auto" unmountOnExit>
              <CardContent paddingbottom="0" sx={{ p: 1 }}>
                <Box sx={{ p: 0, pl: 1 }}>
                  <CardContent sx={{ textAlign: "left", p: 1 }}>
                    <Grid container spacing={2}>
                      <Grid item xs={2}>
                        <Typography variant="p">Rank</Typography>
                      </Grid>
                      <Grid item xs={4}>
                        <Typography variant="p">User</Typography>
                      </Grid>
                      <Grid item xs={3}>
                        <Typography variant="p">Total Solved</Typography>
                      </Grid>
                      <Grid item xs={3}>
                        <Typography variant="p">Total Attempts</Typography>
                      </Grid>
                      {game.users.map((user) => (
                        <>
                          <Grid item xs={2} key={user.id}>
                            <Typography variant="h4" sx={{ mt: 1 }}>
                              {game.users.indexOf(user) + 1}
                            </Typography>
                          </Grid>
                          <Grid item xs={4}>
                            <Box
                              sx={{
                                textAlign: "center",
                                maxWidth: 1 / 3,
                              }}
                            >
                              <Stack
                                direction="row"
                                spacing={2}
                                sx={{ display: "flex", alignItems: "center" }}
                              >
                                <Avatar></Avatar>
                                <Typography variant="body1">
                                  {user.username}
                                </Typography>
                              </Stack>
                            </Box>
                          </Grid>
                          <Grid item xs={3}>
                            <Typography variant="h4" sx={{ mt: 1 }}>
                              {game.users.indexOf(user) + 1}
                            </Typography>
                          </Grid>
                          <Grid item xs={3}>
                            <Typography variant="h4" sx={{ mt: 1 }}>
                              {game.users.indexOf(user) + 1}
                            </Typography>
                          </Grid>
                        </>
                      ))}
                    </Grid>
                  </CardContent>
                </Box>
              </CardContent>
            </Collapse>
          </Card>
        </Box>
      ))}
    </Container>
  );
}

export default CompletedGames;
