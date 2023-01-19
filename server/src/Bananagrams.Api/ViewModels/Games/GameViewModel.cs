namespace Bananagrams.Api.Models.Games;

public class GameViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DateCreated { get; set; }
    public string GameTypeTitle { get; set; }
    public string ChallengerUsername { get; set; }
    public int TotalAnagrams { get; set; }
}