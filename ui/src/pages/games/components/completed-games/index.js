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
import { useState } from "react";
import { useNavigate } from "react-router-dom";

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
  const [expanded, setExpanded] = useState(false);

  const navigate = useNavigate();

  const games = [
    {
      title: "Bananarama",
      totalAnagrams: 5,
      dateCreated: "01/01/2023",
      users: [
        { username: "seancurrie" },
        { username: "davidcurrie" },
        { username: "noahcurrie" },
      ],
    },
    {
      title: "Bananarama",
      totalAnagrams: 5,
      dateCreated: "01/01/2023",
      users: [
        { username: "seancurrie" },
        { username: "davidcurrie" },
        { username: "noahcurrie" },
      ],
    },
    {
      title: "Bananarama",
      totalAnagrams: 5,
      dateCreated: "01/01/2023",
      users: [
        { username: "seancurrie" },
        { username: "davidcurrie" },
        { username: "noahcurrie" },
      ],
    },
  ];

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  const color = "LightPink";

  return (
    <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
      <Typography variant="h2">Completed Games</Typography>
      <Typography variant="p">Game details of completed games</Typography>

      {games.map((game) => (
        <Box alignItems="center" sx={{ mt: 5 }}>
          <Card
            sx={{
              m: "auto",
              border: 1,
              borderRadius: "30px",
              borderColor: color,
              p: 0,
            }}
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
                  <Typography variant="h4">3</Typography>
                </Grid>
                <Grid item xs={3} sx={{ textAlign: "left" }}>
                  <Typography variant="p">Date Created</Typography>
                  <Typography variant="h4">21/01/21</Typography>
                </Grid>
              </Grid>
            </CardContent>

            <Divider variant="middle" />
            <CardActions disableSpacing>
              <ExpandMore
                expand={expanded}
                onClick={handleExpandClick}
                aria-expanded={expanded}
                aria-label="show more"
                sx={{ p: 0, mr: 2 }}
              >
                <ExpandMoreIcon />
              </ExpandMore>
            </CardActions>
            <Collapse in={expanded} timeout="auto" unmountOnExit>
              <CardContent paddingBottom="0" sx={{ p: 1 }}>
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
                          <Grid item xs={2}>
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
