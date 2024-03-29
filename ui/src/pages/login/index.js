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
import Backdrop from "@mui/material/Backdrop";
import { Loader } from "../../components/";
import toast from "react-hot-toast";

const Login = () => {
  const { authDispatch } = AuthContext.useLogin();
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const handleClickShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const login = async () => {
    setIsLoading(true);
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
      toast.error("Failed to logged in!");
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
