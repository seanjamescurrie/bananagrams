using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bananagrams.Dal.Models;

[Table("words")]
public class Word
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("image_location")]
    public string ImageLocation { get; set; }
    [Column("title")]
    public string Title { get; set; }
}