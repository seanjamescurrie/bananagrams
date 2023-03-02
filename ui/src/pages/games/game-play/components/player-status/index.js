import { Avatar, Box } from "@mui/material";
import { Stack } from "@mui/system";
import Icon from "../../../../../components/icon";

function PlayerStatus() {
  return (
    <Box>
      <Stack alignItems="center">
        <Avatar sx={{ mb: 1 }}></Avatar>
        <Icon.SuccessCheckbox></Icon.SuccessCheckbox>
        <Icon.BlankCheckbox></Icon.BlankCheckbox>
        <Icon.BlankCheckbox></Icon.BlankCheckbox>
      </Stack>
    </Box>
  );
}

export default PlayerStatus;
