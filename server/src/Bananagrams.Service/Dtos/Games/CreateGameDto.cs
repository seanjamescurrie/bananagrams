namespace Bananagrams.Service.Dtos;

public class CreateGameDto
{
    public int GameAnagramTypeId { get; set; }
    public int[] PlayerIds { get; set; }
    public string Title { get; set; }
    public int TotalAnagrams { get; set; }
}