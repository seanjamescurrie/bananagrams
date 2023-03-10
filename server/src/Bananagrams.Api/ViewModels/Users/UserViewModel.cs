namespace Bananagrams.Api.ViewModels.Users;

public class UserViewModel
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public int TotalSolved { get; set; }
    public int TotalAttempts { get; set; }
}