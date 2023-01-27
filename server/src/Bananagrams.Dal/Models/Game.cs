using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bananagrams.Dal.Models;

[Table("games")]
public class Game
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("title")]
    public string Title { get; set; }    
    [Column("date_created")]
    public DateTime DateCreated { get; set; }
    public List<GameUser>? GameUsers { get; set; }
    public List<GameAnagram>? GameAnagrams { get; set; }
}