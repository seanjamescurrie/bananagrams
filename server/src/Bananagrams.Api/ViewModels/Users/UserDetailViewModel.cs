using System.Diagnostics.CodeAnalysis;
using Bananagrams.Api.ViewModels.GameUsers;

namespace Bananagrams.Api.ViewModels.Users;

public class UserDetailViewModel
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public DateTime DateCreated { get; set; }
    
    public GameUserViewModel[]? GameUsers { get; set; } 
}