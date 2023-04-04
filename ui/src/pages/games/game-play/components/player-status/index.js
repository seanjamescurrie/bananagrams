import { Avatar, Box, Typography } from "@mui/material";
import { Stack } from "@mui/system";
import { useEffect, useState } from "react";
import Icon from "../../../../../components/icon";

const blank = (i) => {
  return <Icon.BlankCheckbox key={i}></Icon.BlankCheckbox>;
};

const success = (i) => {
  return <Icon.SuccessCheckbox key={i}></Icon.SuccessCheckbox>;
};

const fail = (i) => {
  return <Icon.FailCheckbox key={i}></Icon.FailCheckbox>;
};

const displayBox = (anagram, i) => {
  if (
    !anagram.gameUserGameAnagrams[0].dateSolved &&
    !anagram.gameUserGameAnagrams[0].datePlayed
  )
    return blank(i);

  if (
    !anagram.gameUserGameAnagrams[0].dateSolved &&
    anagram.gameUserGameAnagrams[0].datePlayed
  )
    return fail(i);

  if (anagram.gameUserGameAnagrams[0].dateSolved) return success(i);
};

function PlayerStatus({ user }) {
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    if (user.gameAnagrams != undefined) {
      setIsLoading(false);
    }
    console.log(user.gameAnagrams);
  }, [user.gameAnagrams]);

  return (
    <Box>
      <Stack alignItems="center">
        <Avatar sx={{ mb: 1 }}></Avatar>
        <Typography>{user.username}</Typography>
        <>
          {isLoading ? (
            <p>loading</p>
          ) : (
            user.gameAnagrams.map((anagram, i) => displayBox(anagram, i))
          )}
        </>
      </Stack>
    </Box>
  );
}

export default PlayerStatus;
