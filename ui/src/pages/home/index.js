import { useTheme } from "@emotion/react";
import { Container, Grid, Typography } from "@mui/material";
import Icon from "../../components/icon/index";
import { GameCard } from "./components";
import { Notify } from "./components/notifications";

const Home = () => {
  const theme = useTheme();
  const date = new Date();
  const currentDate = `${date.getDate()} / ${date.getMonth()} / ${date.getFullYear()}`;

  return (
    <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
      <Typography variant="h2" gutterBottom sx={{ mb: 0 }}>
        Start A Game
      </Typography>
      <Typography variant="p" gutterBottom color={"text.secondary"}>
        Select a game type you would like to play
      </Typography>

      <Grid container spacing={2} columns={2} sx={{ mt: 5 }}>
        <Grid item xs={1}>
          <GameCard
            title="Daily Game"
            description={currentDate}
            Image={Icon.Lemon}
            navigation={"/games/daily"}
            color={theme.palette.secondary.main}
          ></GameCard>
        </Grid>
        <Grid item xs={1}>
          <GameCard
            title="Face Off"
            description="Challenge a friend to a game"
            Image={Icon.Cherry}
            navigation={"/games/create"}
            color={theme.palette.secondary.light}
          ></GameCard>
        </Grid>
      </Grid>

      {/* <Notify></Notify> */}
    </Container>
  );
};

export default Home;
