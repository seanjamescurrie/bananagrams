namespace Bananagrams.Service.Dtos.Users;

public class UpdateUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public string? Username { get; set; }
}