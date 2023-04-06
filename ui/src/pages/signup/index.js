import {
  Box,
  Button,
  Container,
  FormControl,
  FormHelperText,
  IconButton,
  InputAdornment,
  InputLabel,
  OutlinedInput,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Notification } from "../../components";
import { UserService } from "../../services";
import { Visibility, VisibilityOff } from "@mui/icons-material";

const SignUp = () => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState("");
  const [displayMessage, setDisplayMessage] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

  const navigate = useNavigate();

  const handleClickShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const createUser = async () => {
    if (password === confirmPassword) {
      const newUser = {
        emailAddress: email,
        firstName: firstName,
        lastName: lastName,
        password: password,
        username: username,
      };

      const response = await UserService.create(newUser);

      if (response === 201) {
        navigate("/login");
      }
    } else {
      setMessage("Passwords do not match");
      setDisplayMessage(true);
    }
  };

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
            onChange={(e) => {
              setFirstName(e.target.value);
            }}
          />
        </div>
        <div>
          <TextField
            required
            fullWidth
            id="outlined-required"
            label="Last Name"
            defaultValue=""
            onChange={(e) => {
              setLastName(e.target.value);
            }}
          />
        </div>
        <div>
          <TextField
            required
            fullWidth
            id="outlined-required"
            label="Email Address"
            defaultValue=""
            onChange={(e) => {
              setEmail(e.target.value);
            }}
          />
        </div>
        <div>
          <TextField
            required
            fullWidth
            id="outlined-required"
            label="Username"
            defaultValue=""
            onChange={(e) => {
              setUsername(e.target.value);
            }}
          />
        </div>
        <FormControl sx={{ width: "100%", mb: 2 }} variant="outlined">
          <InputLabel htmlFor="outlined-adornment-password">
            Password *
          </InputLabel>
          <OutlinedInput
            required
            fullWidth
            id="outlined-adornment-password"
            label="Password"
            defaultValue=""
            type={showPassword ? "text" : "password"}
            endAdornment={
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handleClickShowPassword}
                  onMouseDown={handleMouseDownPassword}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            }
            onChange={(e) => {
              setPassword(e.target.value);
            }}
          />
        </FormControl>
        <FormControl sx={{ width: "100%", mb: 2 }} variant="outlined">
          <InputLabel
            htmlFor="outlined-adornment-confirm-password"
            color={password !== confirmPassword && "error"}
          >
            Confirm Password *
          </InputLabel>
          <OutlinedInput
            required
            fullWidth
            id="outlined-adornment-confirm-password"
            label="Confirm Password"
            defaultValue=""
            error={password !== confirmPassword}
            type={showPassword ? "text" : "password"}
            endAdornment={
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handleClickShowPassword}
                  onMouseDown={handleMouseDownPassword}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            }
            onChange={(e) => {
              setConfirmPassword(e.target.value);
            }}
          />
          {password !== confirmPassword && (
            <FormHelperText error id="accountId-error">
              Passwords do not match
            </FormHelperText>
          )}
        </FormControl>

        <Stack>
          <Button
            onClick={() => createUser()}
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
      {setDisplayMessage && (
        <Notification message={message} display={displayMessage}></Notification>
      )}
    </Container>
  );
};

export default SignUp;
