import { FetchUtils } from "../utils";

const getAll = async () => {
  return await FetchUtils.fetchInstance("games", {
    method: "GET",
  });
};

const getById = async (id) => {
  return await FetchUtils.fetchInstance(`games/${id}`, {
    method: "GET",
  });
};

const create = async (newGame) => {
  return await FetchUtils.fetchInstance(`games`, {
    method: "POST",
    body: JSON.stringify({
      gameAnagramTypeId: newGame.gameAnagramTypeId,
      playerIds: newGame.playerIds,
      title: newGame.title,
      totalAnagrams: newGame.totalAnagrams,
      totalAttempts: newGame.totalAttempts,
    }),
  });
};

const updateAnagramAttempt = async (gameId, updateAttempt) => {
  return await FetchUtils.fetchInstance(
    `games/${gameId}/attempt/${updateAttempt.anagramId}`,
    {
      method: "PUT",
      body: JSON.stringify({
        attempts: updateAttempt.attempts,
        attempt: updateAttempt.attempt,
      }),
    }
  );
};

export default {
  getAll: getAll,
  getById: getById,
  create: create,
  updateAnagramAttempt: updateAnagramAttempt,
};
