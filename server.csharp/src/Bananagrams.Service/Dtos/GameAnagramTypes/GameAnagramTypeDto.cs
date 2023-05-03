namespace Bananagrams.Service.Dtos.GameAnagramTypes;

public class GameAnagramTypeDto
{
    public int Id { get; set; }
    public int MaxAttempts { get; set; }
    public string Title { get; set; }
    public int TimeAllowed { get; set; }
}