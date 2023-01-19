using System.Text.Json.Serialization;

namespace Bananagrams.Api.Dtos;

public class GameAnagramDto
{
    public int Id { get; set; }
    public string AnagramSolution { get; set; }
    public string AnagramWord { get; set; }
    public DateTime DateCreated { get; set; }
    public int Order { get; set; }
    public int GameAnagramTypeId { get; set; }
    public int GameId { get; set; }
    public int WordId { get; set; }
    public GameAnagramTypeDto GameAnagramTypeDto { get; set; }
    public List<GameUserGameAnagramDto> GameUserGameAnagramDtos { get; set; } = new();

}