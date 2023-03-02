import {
  Box,
  Button,
  Container,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { useNavigate } from "react-router-dom";

const SignUp = () => {
  const navigate = useNavigate();

  return (
    <Container maxWidth="sm" sx={{ textAlign: "center", mt: 5 }}>
      <Typography variant="h2" gutterBottom>
        Sign Up
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
        <div>
          <TextField
            required
            fullWidth
            id="outlined-required"
            label="First Name"
            defaultValue=""
          />
        </div>
        <div>
          <TextField
            required
            fullWidth
            id="outlined-required"
            label="Last Name"
            defaultValue=""
          />
        </div>
        <div>
          <TextField
            required
            fullWidth
            id="outlined-required"
            label="Email Address"
            defaultValue=""
          />
        </div>
        <div>
          <TextField
            required
            fullWidth
            id="outlined-required"
            label="Username"
            defaultValue=""
          />
        </div>
        <div>
          <TextField
            required
            fullWidth
            id="outlined-required"
            label="Password"
            defaultValue=""
          />
        </div>
        <div>
          <TextField
            required
            fullWidth
            id="outlined-required"
            label="Confirm Password"
            defaultValue=""
          />
        </div>

        <Stack>
          <Button
            onClick={() => navigate("/")}
            variant="contained"
            fullWidth
            sx={{
              mb: 2,
            }}
          >
            Sign Up
          </Button>
          <Button
            onClick={() => navigate("/login")}
            variant="outlined"
            fullWidth
            sx={{
              mb: 2,
            }}
          >
            Cancel
          </Button>
        </Stack>
      </Box>
    </Container>
  );
};

export default SignUp;
