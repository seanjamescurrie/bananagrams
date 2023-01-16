using System.Diagnostics.CodeAnalysis;
using Bananagrams.Api.Controllers;

namespace Bananagrams.Api.Models;

public class User
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateCreated { get; set; }
}

public class Anagram
{
    public int Id { get; set; }
    public string AnagramWord { get; set; }
    public int AnagramLength { get; set; }
    public string AnagramSolution { get; set; }
    public string ImageLocation { get; set; }
    public DateTime DateCreated { get; set; }
}

public class UserAnagram
{
    public int Id { get; set; }
    public int Attempts { get; set; }
    public DateTime DatePlayed { get; set; }
    public int MaxAttempts { get; set; }
    public bool Solved { get; set; }
    public int TimeAllowed { get; set; }
    public int AnagramId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public Anagram Anagram { get; set; }
}

public class FaceOff
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<UserAnagram> UserAnagrams { get; set; } = new();
}