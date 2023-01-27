namespace Bananagrams.Service.Dtos.GameUserGameAnagrams;

public class UpdateGameUserGameAnagramDto
{
    public int Attempts { get; set; }
    public DateTime DatePlayed { get; set; }
    public DateTime? DateSolved { get; set; }
}