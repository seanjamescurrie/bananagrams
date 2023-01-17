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

public class GameUser
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public List<GameUserGameAnagram> GameUserGameAnagram { get; set; } = new List<GameUserGameAnagram>();

}

public class GameAnagram
{
    public int Id { get; set; }
    public string AnagramSolution { get; set; }
    public string AnagramWord { get; set; }
    public DateTime DateCreated { get; set; }
    public int Order { get; set; }
    public int GameAnagramTypeId { get; set; }
    public int GameId { get; set; }
    public int WordId { get; set; }
    public GameAnagramType GameAnagramType { get; set; }
    public List<GameUserGameAnagram> GameUserGameAnagram { get; set; } = new List<GameUserGameAnagram>();

}

public class GameUserGameAnagram
{
    public int Id { get; set; }
    public int Attempts { get; set; }
    public DateTime DatePlayed { get; set; }
    public bool Solved { get; set; }
    public int GameAnagramId { get; set; }
    public int GameUserId { get; set; }
    public GameUser GameUser { get; set; }
    public GameAnagram GameAnagram { get; set; }
}

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DateCreated { get; set; }
    public List<GameAnagram> GameAnagrams { get; set; } = new List<GameAnagram>();
}

public class Word
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageLocation { get; set; }
}

public class GameAnagramType
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int MaxAttempts { get; set; }
    public int TimeAllowed { get; set; }
}