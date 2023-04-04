import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import {
  Avatar,
  Card,
  CardActionArea,
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
import { useNavigate } from "react-router-dom";
import { GameService } from "../../../../services";

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

const OpenLobbies = forwardRef((props, ref) => {
  const theme = useTheme();
  const [games, setGames] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  const navigate = useNavigate();

  async function fetchData() {
    const response = await GameService.getAll();
    if (response.status === 200) {
      const data = await response.json();
      let foundGames = data
        .filter(
          (game) => game.gameAnagramType.id === 2 && game.completed === false
        )
        .map((game) => ({
          id: game.id,
          title: game.title,
          dateCreated: game.dateCreated,
          totalAnagrams: game.totalAnagrams,
          users: game.gameUsers.map((user) => ({
            username: user.username,
          })),
          expanded: false,
        }));
      setGames(foundGames);
      setIsLoading(false);
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
      totalAnagrams: game.totalAnagrams,
      users: game.users.map((user) => ({
        username: user.username,
      })),
      expanded: game.id === id ? !game.expanded : game.expanded,
    }));

    setGames(updated);
  };

  return (
    <Box ref={ref} {...props} width="1">
      <Container sx={{ textAlign: "center", mt: 5 }}>
        {/* <Typography variant="h2">Lobbies</Typography>
      <Typography variant="p" color={"text.secondary"}>
        Game lobbies waiting on player replies
      </Typography> */}

        {isLoading ? (
          <Loader></Loader>
        ) : (
          <>
            {games.map((game) => (
              <Box
                alignItems="center"
                sx={{ mt: 5 }}
                key={`open-lobbies-${game.id}`}
              >
                <Card
                  sx={{
                    m: "auto",
                    border: 1,
                    borderRadius: "30px",
                    borderColor: theme.palette.success.main,
                    p: 0,
                  }}
                >
                  <CardActionArea
                    onClick={() => navigate(`/games/${game.id}/lobby`)}
                  >
                    <CardContent paddingBottom="0">
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
                  </CardActionArea>

                  <Divider variant="middle" />
                  <CardActions disableSpacing>
                    <ExpandMore
                      expand={game.expanded}
                      onClick={() => handleExpandClick(game.id)}
                      aria-label="show more"
                      sx={{ p: 0, mr: 2 }}
                    >
                      <ExpandMoreIcon />
                    </ExpandMore>
                  </CardActions>
                  <Collapse in={game.expanded} timeout="auto" unmountOnExit>
                    <CardContent paddingBottom="0" sx={{ p: 1 }}>
                      <Box sx={{ p: 0, pl: 1 }}>
                        <CardContent sx={{ textAlign: "left", p: 1 }}>
                          <Typography variant="p">Users</Typography>
                          <Grid container spacing={2}>
                            {game.users.map((user) => (
                              <Grid item xs={4} key={`${user.username}`}>
                                <Box
                                  sx={{
                                    mt: 2,
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
                                    <Typography variant="h4" sx={{ mt: 1 }}>
                                      {game.users.indexOf(user) + 1}
                                    </Typography>
                                    <Avatar></Avatar>
                                    <Typography variant="body1">
                                      {user.username}
                                    </Typography>
                                  </Stack>
                                </Box>
                              </Grid>
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
        )}
      </Container>
    </Box>
  );
});

export default OpenLobbies;
