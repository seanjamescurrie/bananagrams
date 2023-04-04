import {
  Divider,
  Table,
  TableBody,
  TableCell,
  tableCellClasses,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import { Container } from "@mui/system";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import Paper from "@mui/material/Paper";
import styled from "@emotion/styled";
import { Loader } from "../../../components";

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.text.primary,
    color: theme.palette.secondary.contrastText,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const Results = () => {
  const params = useParams();

  const [game, setGame] = useState({});
  const [isLoading, setIsLoading] = useState(true);
  const [timer, setTimer] = useState(10);

  const fetchData = async () => {
    const response = await fetch(`http://localhost:5016/games/${params.id}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (response.status === 200) {
      let data = await response.json();
      let foundGame = {
        id: data.id,
        title: data.title,
        dateCreated: data.dateCreated,
        totalAnagrams: data.gameAnagrams.length,
        users: data.gameUsers.map((user) => ({
          username: user.username,
          totalSolved: user.totalSolved,
          totalAttempts: user.totalAttempts,
        })),
        totalPossibleAttempts:
          data.gameAnagramType.maxAttempts * data.gameAnagrams.length,
      };
      setGame(foundGame);
    }
  };

  useEffect(() => {
    timer > 0 && setTimeout(() => setTimer(timer - 1), 1000);
    if (timer <= 0) {
      setIsLoading(false);
    }
  }, [timer]);

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <>
      <Container maxWidth="lg" sx={{ textAlign: "center", mt: 5 }}>
        <Typography variant="body1" color={"text.secondary"} sx={{ mb: 1 }}>
          Results
        </Typography>
        <Typography variant="h2" sx={{ mb: 0 }}>
          {game.title}
        </Typography>

        <Divider sx={{ mt: 4 }} />
        {isLoading ? (
          <Loader></Loader>
        ) : (
          <>
            <TableContainer component={Paper}>
              <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                  <TableRow>
                    <StyledTableCell align="center">
                      <Typography variant="p">Rank</Typography>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <Typography variant="p">User</Typography>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <Typography variant="p">Total Solved</Typography>
                    </StyledTableCell>
                    <StyledTableCell align="center">
                      <Typography variant="p">Total Attempts</Typography>
                    </StyledTableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {game.users.map((user) => (
                    <TableRow
                      key={user.username}
                      sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                    >
                      <TableCell component="th" scope="row" align="center">
                        <Typography variant="h4" sx={{ mt: 1 }}>
                          {game.users.indexOf(user) + 1}
                        </Typography>
                      </TableCell>
                      <TableCell align="center">
                        <Typography variant="h4">{user.username}</Typography>
                      </TableCell>
                      <TableCell align="center">
                        <Typography variant="h4" sx={{ mt: 1 }}>
                          {user.totalSolved}
                        </Typography>
                      </TableCell>
                      <TableCell align="center">
                        <Typography variant="h4" sx={{ mt: 1 }}>
                          {user.totalAttempts}
                        </Typography>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </>
        )}
      </Container>
    </>
  );
};

export default Results;
