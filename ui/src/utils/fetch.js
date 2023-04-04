import fetchIntercept from "fetch-intercept";
import { AuthenticationService, StorageService } from "../services";
import LoginUtils from "./login";
import toast from "react-hot-toast";

const baseUrl = process.env.REACT_APP_API_URL ?? "http://localhost:5016";
const configureUrl = (url) => `${baseUrl}/${url}`;
const refreshUrl = "/authentication/refresh";
const authUrl = "/authentication";
let isRefreshing = false;

fetchIntercept.register({
  request: function (url, config) {
    config = {
      ...config,
      headers: {
        "Content-Type": "application/json",
      },
    };
    if (config.inferHeaders) {
      delete config.headers;
    }
    const token = StorageService.getLocalStorage("auth");
    if (token && !LoginUtils.isTokenExpired(token)) {
      const bearerToken =
        url === `${baseUrl}${refreshUrl}`
          ? token.refreshToken
          : token.accessToken;
      config.headers = {
        ...config.headers,
        Authorization: `Bearer ${bearerToken}`,
      };
    }
    return [url, config];
  },
  //   response: function (response) {
  //     if (
  //       (response.status === 401 &&
  //         response.request.url !== `${baseUrl}${refreshUrl}`) ||
  //       response.request.url !== `${baseUrl}${authUrl}`
  //     ) {
  //       const refreshToken = StorageService.getLocalStorage("auth")?.refreshToken;
  //       if (refreshToken && !isRefreshing) {
  //         isRefreshing = true;
  //         const originalConfig = response.request;
  //         AuthenticationService.refresh()
  //           .then((response) => {
  //             response.json().then((content) => {
  //               toast.success("session expired and was successfully refreshed");
  //               StorageService.setLocalStorage("auth", content);
  //               return fetchInstance(originalConfig.url, originalConfig);
  //             });
  //           })
  //           .catch(() => {
  //             toast.error(
  //               "something has gone wrong with authentication. logging out..."
  //             );
  //             StorageService.removeLocalStorage("auth");
  //             setTimeout(() => {
  //               window.location.reload();
  //             }, 2000);
  //           })
  //           .finally(() => {
  //             isRefreshing = false;
  //           });
  //       }
  //     }
  //     return response;
  //   },
});

const fetchInstance = async (url, ...params) => {
  return await fetch(configureUrl(url), ...params);
};

export default { fetchInstance };
