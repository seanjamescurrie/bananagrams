namespace Bananagrams.Api.Dtos;

public class GameUserDto
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public List<GameUserGameAnagramDto> GameUserGameAnagramDtos { get; set; } = new();

}