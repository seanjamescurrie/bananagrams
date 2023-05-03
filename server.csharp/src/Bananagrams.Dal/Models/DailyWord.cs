using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Bananagrams.Dal.Models;

[Table("daily_words")]
public class DailyWord
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("anagram")]
    public string Anagram { get; set; }
    [Column("date_created")]
    public DateTime DateCreated { get; set; }
    [Column("word_id")]
    public int WordId { get; set; }
    [AllowNull]
    public Word Word { get; set; }
}