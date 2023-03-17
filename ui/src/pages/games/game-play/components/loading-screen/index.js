import { Box } from "@mui/system";
import DailyAnagramLoader from "../../../../../assets/daily-anagram-loop.gif";
import FaceOffAnagramLoader from "../../../../../assets/face-off-loop.gif";

function LoadingScreen({ type }) {
  const loader = type == "daily" ? DailyAnagramLoader : FaceOffAnagramLoader;
  return (
    <>
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          justifyContent: "space-around",
          height: "80vh",
        }}
      >
        <img src={loader} loading="lazy" width="50%" />
      </Box>
    </>
  );
}

export default LoadingScreen;
