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
import { forwardRef, useEffect, useState } from "react";
import * as dayjs from "dayjs";
import { useTheme } from "@emotion/react";
import { Loader } from "../../../../components";
import { GameService } from "../../../../services";
import Icon from "../../../../components/icon";

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

const CompletedGames = forwardRef((props, ref) => {
  const theme = useTheme();
  const [expanded, setExpanded] = useState(false);
  const [games, setGames] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  async function fetchData() {
    const response = await GameService.getAll();

    if (response.status === 200) {
      const data = await response.json();
      console.log(data);
      let foundGames = data
        .filter((game) => game.completed === true)
        .map((game) => ({
          id: game.id,
          title: game.title,
          dateCreated: game.dateCreated,
          totalAnagrams: game.totalAnagrams,
          users: game.gameUsers.map((user) => ({
            username: user.username,
            totalSolved: user.totalSolved,
            totalAttempts: user.totalAttempts,
          })),
          expanded: false,
        }));
      setGames(foundGames);
    }
  }

  useEffect(() => {
    fetchData();
    setIsLoading(false);
  }, []);

  const handleExpandClick = (id) => {
    let updated = games.map((game) => ({
      id: game.id,
      title: game.title,
      dateCreated: game.dateCreated,
      totalAnagrams: game.totalAnagrams,
      users: game.users.map((user) => ({
        username: user.username,
        totalSolved: user.totalSolved,
        totalAttempts: user.totalAttempts,
      })),
      expanded: game.id === id ? !game.expanded : game.expanded,
    }));

    setGames(updated);
  };

  return (
    <Box ref={ref} {...props} width="1">
      <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
        {isLoading ? (
          <Loader></Loader>
        ) : games.length > 0 ? (
          <>
            {games.map((game) => (
              <Box
                alignItems="center"
                sx={{ mt: 5 }}
                key={`complete-games-${game.id}`}
              >
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
                        <Typography variant="h4">
                          {game.totalAnagrams}
                        </Typography>
                      </Grid>
                      <Grid item xs={3} sx={{ textAlign: "left" }}>
                        <Typography variant="p">Date Created</Typography>
                        <Typography variant="h4">
                          {dayjs(game.dateCreated)
                            .format("DD-MM-YYYY")
                            .substring(0, 10)}
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
                              <Typography variant="p">
                                Total Attempts
                              </Typography>
                            </Grid>
                            {game.users.map((user) => (
                              <>
                                <Grid item xs={2} key={user.username}>
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
                                      sx={{
                                        display: "flex",
                                        alignItems: "center",
                                      }}
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
                                    {user.totalSolved}
                                  </Typography>
                                </Grid>
                                <Grid item xs={3}>
                                  <Typography variant="h4" sx={{ mt: 1 }}>
                                    {user.totalAttempts}
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
          </>
        ) : (
          <Box sx={{ mt: 10 }}>
            <Icon.Banana width="200" height="200"></Icon.Banana>
            <Typography variant="h5" color={"text.secondary"}>
              No games completed
            </Typography>
          </Box>
        )}
      </Container>
    </Box>
  );
});

export default CompletedGames;
