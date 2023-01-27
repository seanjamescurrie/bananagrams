using System.Diagnostics.CodeAnalysis;

namespace Bananagrams.Service.Dtos.DailyWords;

public class DailyWordDto
{
    public int Id { get; set; }
    public string Anagram { get; set; }
    public DateTime DateCreated { get; set; }
    public int WordId { get; set; }
    public WordDto? Word { get; set; }
}