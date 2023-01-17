using Bananagrams.Api.Controllers;

namespace Bananagrams.Api.Models;

public class GameViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DateCreated { get; set; }
    public string GameType { get; set; }
    public List<GameUser> GameUsers { get; set; } = new();
    public List<GameAnagram> GameAnagrams { get; set; } = new();
}

public class GameUserViewModel
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public List<GameUserGameAnagramViewModel> GameUserGameAnagram { get; set; } = new List<GameUserGameAnagramViewModel>();

}

public class GameUserGameAnagramViewModel
{
    
}