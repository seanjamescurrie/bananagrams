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
import {
  UserService,
  AuthenticationService,
  StorageService,
} from "../../services";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import Backdrop from "@mui/material/Backdrop";
import { Loader } from "../../components/";
import { AuthContext } from "../../contexts";
import toast from "react-hot-toast";

const SignUp = () => {
  const { authDispatch } = AuthContext.useLogin();

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState("");
  const [displayMessage, setDisplayMessage] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const navigate = useNavigate();

  const handleClickShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const createUser = async () => {
    setIsLoading(true);
    if (password === confirmPassword) {
      const newUser = {
        emailAddress: email,
        firstName: firstName,
        lastName: lastName,
        password: password,
        username: username,
      };

      const response = await UserService.create(newUser);

      if (response.status === 201) {
        const response = await AuthenticationService.authenticate(
          email,
          password
        );

        if (response.status === 200) {
          const data = await response.json();
          StorageService.setLocalStorage("auth", data);
          StorageService.setLocalStorage("email", email);
          authDispatch({
            type: "authentication",
            ...data,
          });
          toast.success("Successfully logged in!");
          navigate("/");
        }
      }
    } else {
      setMessage("Passwords do not match");
      setDisplayMessage(true);
    }
    setIsLoading(false);
  };

  return (
    <Container maxWidth="sm" sx={{ textAlign: "center", mt: 5 }}>
      <Backdrop
        sx={{ color: "#fff", zIndex: (theme) => theme.zIndex.drawer + 1 }}
        open={isLoading}
      >
        <Loader />
      </Backdrop>
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
