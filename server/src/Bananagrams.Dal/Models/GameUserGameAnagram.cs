using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Bananagrams.Dal.Models;

[Table("game_user_game_anagrams")]
public class GameUserGameAnagram
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("attempts")]
    public int Attempts { get; set; }
    [Column("date_played")]
    public DateTime? DatePlayed { get; set; }
    [Column("date_solved")]
    public DateTime? DateSolved { get; set; }
    [Column("game_user_id")]
    public int GameUserId { get; set; }
    [Column("game_anagram_id")]
    public int GameAnagramId { get; set; }
    public GameUser GameUser { get; set; }    
    public GameAnagram GameAnagram { get; set; }
}