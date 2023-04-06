import { FetchUtils } from "../utils";

const getAll = async () => {
  return await FetchUtils.fetchInstance("users", {
    method: "GET",
  });
};

const getById = async (id) => {
  return await FetchUtils.fetchInstance(`users/${id}`, {
    method: "GET",
  });
};

const create = async (newUser) => {
  return await FetchUtils.fetchInstance(`users`, {
    method: "POST",
    body: JSON.stringify({
      emailAddress: newUser.emailAddress,
      firstName: newUser.firstName,
      lastName: newUser.lastName,
      password: newUser.password,
      username: newUser.username,
    }),
  });
};

export default {
  getAll: getAll,
  getById: getById,
  create: create,
};
