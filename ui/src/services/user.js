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

export default {
  getAll: getAll,
  getById: getById,
};
