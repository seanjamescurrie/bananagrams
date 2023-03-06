import { Box } from "@mui/system";
import DailyAnagramLoader from "../../../../../assets/DailyAnagrams.gif";

const LoadingScreen = () => (
  <>
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        justifyContent: "space-around",
        height: "80vh",
        width: "80vw",
      }}
    >
      <img src={DailyAnagramLoader} loading="lazy" />
    </Box>
  </>
);

export default LoadingScreen;
