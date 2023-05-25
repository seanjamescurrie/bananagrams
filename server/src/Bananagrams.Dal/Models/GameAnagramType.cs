using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bananagrams.Dal.Models;

[Table("game_anagram_types")]
public class GameAnagramType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("max_attempts")]
    public int MaxAttempts { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("time_allowed")]
    public int TimeAllowed { get; set; }
}