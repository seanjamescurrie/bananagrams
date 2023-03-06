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
import * as dayjs from "dayjs";

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

function OpenLobbies({ games }) {
  const [expanded, setExpanded] = useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  const color = "LightPink";

  return (
    <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
      {/* <Typography variant="h2">Lobbies</Typography>
      <Typography variant="p" color={"text.secondary"}>
        Game lobbies waiting on player replies
      </Typography> */}

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
            <CardActionArea>
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
                    <Typography variant="h4">{game.totalAnagrams}</Typography>
                  </Grid>
                  <Grid item xs={3} sx={{ textAlign: "left" }}>
                    <Typography variant="p">Date Created</Typography>
                    <Typography variant="h4">
                      {dayjs().format(game.dateCreated).substring(0, 10)}
                    </Typography>
                  </Grid>
                </Grid>
              </CardContent>
            </CardActionArea>

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
                    <Typography variant="p">Users</Typography>
                    <Grid container spacing={2}>
                      {game.users.map((user) => (
                        <Grid item xs={4}>
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
                              sx={{ display: "flex", alignItems: "center" }}
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
    </Container>
  );
}

export default OpenLobbies;
