namespace Bananagrams.Api.ViewModels.GameUserGameAnagrams;

public class GameUserGameAnagramResponseViewModel
{
    public int Attempts { get; set; }
    public bool isSolved { get; set; }
    public int GameId { get; set; } 
    public int GameAnagramId { get; set; }
    public int UserId { get; set; }
}