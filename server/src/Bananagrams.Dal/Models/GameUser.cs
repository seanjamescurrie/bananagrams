using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Bananagrams.Dal.Models;

[Table("game_users")]
public class GameUser
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("game_id")]
    public int GameId { get; set; }
    [Column("user_id")]
    public int UserId { get; set; }
    public Game? Game { get; set; }
    public User? User { get; set; }
    public List<GameUserGameAnagram>? GameUserGameAnagrams { get; set; }
}