using Bananagrams.Service.Dtos.GameUsers;

namespace Bananagrams.Service.Dtos.Users;

public class UserDto
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string? EmailAddress { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    
    public GameUserDto[]? GameUsers { get; set; }
}