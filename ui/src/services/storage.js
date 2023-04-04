const getLocalStorage = (identifier) => {
  const auth = localStorage.getItem(identifier);
  if (!auth) return null;

  return JSON.parse(auth);
};

const setLocalStorage = (identifier, value) => {
  if (value) {
    localStorage.setItem(identifier, JSON.stringify(value));
  }
};

const removeLocalStorage = (identifier) => {
  localStorage.removeItem(identifier);
};

export default {
  getLocalStorage,
  setLocalStorage,
  removeLocalStorage,
};
