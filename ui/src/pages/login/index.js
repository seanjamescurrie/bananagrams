import { Visibility, VisibilityOff } from "@mui/icons-material";
import {
  Box,
  Button,
  Container,
  FormControl,
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
import { AuthContext } from "../../contexts";
import { AuthenticationService, StorageService } from "../../services";
import toast from "react-hot-toast";

const Login = () => {
  const { authDispatch } = AuthContext.useLogin();
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const login = async () => {
    // const response = await fetch("http://localhost:5016/authentication", {
    //   method: "POST",
    //   headers: {
    //     "Content-Type": "application/json",
    //   },
    //   body: JSON.stringify({
    //     email: email,
    //     password: password,
    //   }),
    // });

    const response = await AuthenticationService.authenticate(email, password);

    if (response.status === 200) {
      const data = await response.json();
      console.log(data);
      StorageService.setLocalStorage("auth", data);
      StorageService.setLocalStorage("email", email);
      authDispatch({
        type: "authentication",
        ...data,
      });
      toast.success("Successfully logged in!");
      navigate("/");
    } else {
      console.log("error");
    }
  };

  // const authentication = async () => {
  //   const response = await AuthenticationService.authenticate(email, password);
  //   if (response.status === 200) {
  //     const loginResult = await response.json();
  //     StorageService.setLocalStorage(loginResult, StorageTypes.AUTH);
  //     StorageService.setLocalStorage(email, StorageTypes.EMAIL);
  //     dispatch({
  //       type: "authentication",
  //       ...loginResult,
  //     });
  //     toast.success("Successfully logged in!");
  //     navigate(NavigationRoutes.Home);
  //   } else {
  //     toast.error("Failed to logged in!");
  //     navigate(NavigationRoutes.Login);
  //   }
  // };

  return (
    <Container maxWidth="sm" sx={{ textAlign: "center", mt: 5 }}>
      <Typography variant="h2" gutterBottom>
        Login
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
        <TextField
          required
          fullWidth
          id="outlined-required"
          label="Username/Email"
          defaultValue=""
          onChange={(e) => {
            setEmail(e.target.value);
          }}
        />
        {/* <TextField
          required
          fullWidth
          id="outlined-required"
          label="Password"
          type="password"
          defaultValue=""
          onChange={(e) => {
            setPassword(e.target.value);
          }}
        /> */}
        <FormControl sx={{ width: "100%", mb: 2 }} variant="outlined">
          <InputLabel htmlFor="outlined-adornment-password">
            Password *
          </InputLabel>
          <OutlinedInput
            required
            fullWidth
            id="outlined-adornment-password"
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
            label="Password *"
            defaultValue=""
            onChange={(e) => {
              setPassword(e.target.value);
            }}
          />
        </FormControl>
        <Stack>
          <Button
            onClick={() => login()}
            variant="contained"
            fullWidth
            sx={{
              mb: 2,
            }}
          >
            Login
          </Button>
          <Button
            onClick={() => navigate("/signup")}
            variant="outlined"
            fullWidth
            sx={{
              mb: 2,
            }}
          >
            Sign Up
          </Button>
        </Stack>
      </Box>
    </Container>
  );
};

export default Login;
