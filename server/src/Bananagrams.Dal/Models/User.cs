using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Bananagrams.Dal.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("date_created")]
    public DateTime DateCreated { get; set; }
    [Column("email_address")]
    public string EmailAddress { get; set; }
    [Column("first_name")]
    public string FirstName { get; set; }
    [Column("last_name")]
    public string LastName { get; set; }
    [Column("user_name")]
    public string Username { get; set; }
    public List<GameUser>? GameUsers { get; set; }
}