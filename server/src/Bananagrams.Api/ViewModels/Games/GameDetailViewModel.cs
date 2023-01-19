using System.Text.Json.Serialization;
using Bananagrams.Api.Dtos;

namespace Bananagrams.Api.Models.Games;

public class GameDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DateCreated { get; set; }
    public int GameAnagramTypeId { get; set; }
    public GameAnagramTypeDto GameAnagramTypeDto { get; set; }
    public List<GameUserDto> GameUserDtos { get; set; } = new();
    public List<GameAnagramDto> GameAnagramDtos { get; set; } = new();
}