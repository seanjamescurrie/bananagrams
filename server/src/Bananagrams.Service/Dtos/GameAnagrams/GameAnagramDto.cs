using Bananagrams.Service.Dtos.DailyWords;
using Bananagrams.Service.Dtos.GameAnagramTypes;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;

namespace Bananagrams.Service.Dtos.GameAnagrams;

public class GameAnagramDto
{
    public int Id { get; set; }
    public string? AnagramWord { get; set; }
    public DateTime DateCreated { get; set; }
    public int Order { get; set; }
    public int GameId { get; set; }
    public int GameAnagramTypeId { get; set; }
    public int WordId { get; set; }
    
    public GameDto? Game { get; set; }
    public GameAnagramTypeDto? GameAnagramType { get; set; }
    public WordDto? Word { get; set; }
    public GameUserGameAnagramDto[]? GameUserGameAnagrams { get; set; }
}