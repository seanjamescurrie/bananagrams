using Bananagrams.Dal.Contexts;
using Bananagrams.Dal.Models;

namespace Bananagrams.Api.Integration.Test.Base;

public static class DatabaseSeed
{
    public static void SeedDatabase(BananagramsDatabase database)
    {
        var gameUserGameAnagram = new GameUserGameAnagram
        {
            Id = 1,
            Attempts = 0,
            DatePlayed = DateTime.UtcNow,
            GameAnagramId = 1,
            GameUserId = 1,
        };
        
        var gameAnagram = new GameAnagram
        {
            Id = 1,
            AnagramWord = "",
            DateCreated = DateTime.UtcNow,
            GameId = 1,
            GameAnagramTypeId = 1,
            Order = 1,
            GameUserGameAnagrams = new List<GameUserGameAnagram>
            {
                gameUserGameAnagram
            }
        };

        var gameUser = new GameUser
        {
            Id = 1,
            GameId = 1,
            UserId = 1,
            GameUserGameAnagrams = new List<GameUserGameAnagram>
            {
                gameUserGameAnagram
            }
        };
        
        var game = new Game
        {
            Id = 1,
            Title = "Test",
            DateCreated = DateTime.UtcNow,
            GameAnagrams = new List<GameAnagram>
            {
                gameAnagram
            },
            GameUsers = new List<GameUser>
            {
                gameUser
            }
        };

        database.Add(game);
        database.SaveChanges();

        var user = new User
        {
            Id = 1,
            DateCreated = DateTime.UtcNow,
            EmailAddress = "mail@mail.com",
            FirstName = "Sean",
            LastName = "Currie",
            Username = "seancurrie",
            Password = "Password123!"
        };

        database.Add(user);
        database.SaveChanges();
        
        var gameAnagramType = new GameAnagramType()
        {
            Id = 1,
            Title = "Daily",
            MaxAttempts = 5,
            TimeAllowed = 0
        };

        database.Add(gameAnagramType);
        database.SaveChanges();
    }
    
    
}