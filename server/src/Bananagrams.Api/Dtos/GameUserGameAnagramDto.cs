namespace Bananagrams.Api.Dtos;

public class GameUserGameAnagramDto
{
    public int Id { get; set; }
    public int Attempts { get; set; }
    public DateTime DatePlayed { get; set; }
    public DateTime DateSolved { get; set; }
    public int GameAnagramId { get; set; }
    public int GameUserId { get; set; }
    public GameUserDto GameUserDto { get; set; }
    public GameAnagramDto GameAnagramDto { get; set; }
}