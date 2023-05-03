using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bananagrams.Dal.Models;

[Table("game_anagrams")]
public class GameAnagram
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("anagram_word")]
    public string AnagramWord { get; set; }
    [Column("date_created")]
    public DateTime DateCreated { get; set; }
    [Column("order_sequence")]
    public int Order { get; set; }
    [Column("game_id")]
    public int GameId { get; set; }
    [Column("game_anagram_type_id")]
    public int GameAnagramTypeId { get; set; }
    [Column("word_id")]
    public int WordId { get; set; }
    
    public Game? Game { get; set; }
    public GameAnagramType? GameAnagramType { get; set; }
    public Word? Word { get; set; }
    public List<GameUserGameAnagram>? GameUserGameAnagrams { get; set; }
}