import { FetchUtils } from "../utils/";

const authenticate = async (email, password) => {
  return await FetchUtils.fetchInstance("authentication", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });
};

const refresh = async () => {
  return await FetchUtils.fetchInstance("authentication/refresh", {
    method: "GET",
  });
};

export default {
  authenticate: authenticate,
  refresh: refresh,
};
