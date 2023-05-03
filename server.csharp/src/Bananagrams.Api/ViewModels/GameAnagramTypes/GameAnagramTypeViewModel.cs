namespace Bananagrams.Api.ViewModels.GameAnagramTypes;

public class GameAnagramTypeViewModel
{
    public int Id { get; set; }
    public int MaxAttempts { get; set; }
    public int TimeAllowed { get; set; }
    public string Title { get; set; }
}