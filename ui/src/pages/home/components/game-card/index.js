import { Card, CardActionArea, CardContent, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";

function GameCard({ title, description, Image, navigation, color }) {
  const navigate = useNavigate();
  return (
    <>
      <Card
        sx={{
          maxWidth: 500,
          maxHeight: 500,
          margin: "auto",
          border: 1,
          borderRadius: "16px",
          borderColor: color,
        }}
      >
        <CardActionArea onClick={() => navigate(navigation)}>
          <CardContent sx={{ mt: 5, mb: 5 }}>
            <Image
              component="img"
              height="200"
              width="200"
              // Can alter colour of svg - find a way to do using theme options and light/dark mode
              style={{ color: color }}
            />
            <Typography
              gutterBottom
              variant="h4"
              component="div"
              sx={{ color: color, mb: 0 }}
            >
              {title}
            </Typography>
            <Typography variant="body2" sx={{ color: color, mb: 0 }}>
              {description}
            </Typography>
          </CardContent>
        </CardActionArea>
      </Card>
    </>
  );
}

export default GameCard;
