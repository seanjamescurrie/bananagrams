namespace Bananagrams.Service.Dtos.GameUserGameAnagrams;

public class UpdateGameUserGameAnagramDto
{
    public string Attempt { get; set; }
    public int Attempts { get; set; }
    public DateTime DatePlayed { get; set; }
    public DateTime? DateSolved { get; set; }
}