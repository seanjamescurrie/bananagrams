using Bananagrams.Api.ViewModels.GameUserGameAnagrams;

namespace Bananagrams.Api.ViewModels.GameAnagrams;

public class GameAnagramViewModel
{
    public int Id { get; set; }
    public string AnagramWord { get; set; }
    public DateTime DateCreated { get; set; }
    public int Order { get; set; }
    public int GameId { get; set; }
    public int WordId { get; set; }
    public List<GameUserGameAnagramViewModel>? GameUserGameAnagrams { get; set; }

}