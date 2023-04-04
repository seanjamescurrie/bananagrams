import jwtDecode from "jwt-decode";

const isTokenExpired = (token) => {
  if (!token || !token.accessToken || !token.refreshToken) return true;
  const accessJwt = jwtDecode(token.accessToken);
  const currentTime = new Date().getTime() / 1000;

  if (currentTime < accessJwt.exp) return false;

  const refreshJwt = jwtDecode(token.refreshToken);

  if (currentTime < refreshJwt.exp) return false;

  return true;
};

const getEmail = (accessToken) => {
  if (!accessToken) return false;

  const {
    claims: { email },
  } = jwtDecode(accessToken);

  return email;
};

const getAccountUsernameDisplay = (accessToken) => {
  if (!accessToken) return "";

  const {
    claims: { username },
  } = jwtDecode(accessToken);

  return `${username}`;
};

const getAccountId = (accessToken) => {
  if (!accessToken) return false;

  const { sub } = jwtDecode(accessToken);

  return sub;
};

const getAccountIdRefresh = (refreshToken) => {
  if (!refreshToken) return false;

  const { sub } = jwtDecode(refreshToken);

  if (sub == undefined) return false;

  return sub;
};

const LoginUtils = {
  isTokenExpired,
  getEmail,
  getAccountUsernameDisplay,
  getAccountId,
  getAccountIdRefresh,
};

export default LoginUtils;
