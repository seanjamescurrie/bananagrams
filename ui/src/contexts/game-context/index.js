import { createContext } from "react";

export const CreateGameContext = createContext({
  createGame: {
    userIds: [],
    rules: { title: "", totalAnagrams: 0, totalAttempts: 0 },
  },
  setCreateGame: () => {},
});

export const AnagramAttemptContext = createContext({
  anagramAttempt: { attempt: "", anagramId: 0, attempts: 0 },
  setAnagramAttempt: () => {},
});

export const AnagramRowContext = createContext({
  anagramRow: 0,
  setAnagramRow: () => {},
});
