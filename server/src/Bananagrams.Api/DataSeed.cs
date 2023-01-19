using System.Text.Json.Serialization;
using Bananagrams.Api.Dtos;
using Bananagrams.Api.Models.Games;
using Bananagrams.Api.ViewModels.Users;

namespace Bananagrams.Api;

public class DataSeed
{
    //Dtos

    public static GameAnagramDto gameAnagramDto = new()
    {
        AnagramSolution = "BANANA",
        AnagramWord = "ANANAB",
        DateCreated = DateTime.Now,
        GameAnagramTypeId = 2,
        GameAnagramTypeDto = gameAnagramTypeDto

    };

    public static GameUserDto gameUserDto = new()
    {
        UserId = 1,
        GameId = 1,
        GameUserGameAnagramDtos = new()
        {
            gameUserGameAnagramDto,
            gameUserGameAnagramDto
        }
    };

    public static GameAnagramTypeDto gameAnagramTypeDto = new()
    {
        Id = 1,
        Title = "Face Off",
        MaxAttempts = 3,
        TimeAllowed = 30
    };

    public static GameUserGameAnagramDto gameUserGameAnagramDto = new()
    {
        Attempts = 3,
        DatePlayed = DateTime.Now,
        GameUserId = 1,
        GameAnagramId = 1,
        GameUserDto = gameUserDto,
        GameAnagramDto = gameAnagramDto
    };
    
    // view models
    public static UserViewModel user = new()
    {
        EmailAddress = "sean@mail.com",
        FirstName = "Sean",
        LastName = "Currie",
        Username = "seanjcurrie"
    };

    public static UserDetailViewModel detailUser = new()
    {
        DateCreated = DateTime.Now,
        EmailAddress = "sean@mail.com",
        FirstName = "Sean",
        LastName = "Currie",
        Username = "seanjcurrie"
    };

    public static GameViewModel game = new()
    {
        Id = 1,
        Title = "Sean Vs Niall",
        DateCreated = DateTime.Now,
        TotalAnagrams = 2,
        ChallengerUsername = "Niall",
        GameTypeTitle = "Face Off"
    };

    public static GameDetailViewModel gameDetail = new()
    {
        Id = 1,
        DateCreated = DateTime.Now,
        GameAnagramTypeId = 1,
        Title = "Sean Vs David",
        GameAnagramDtos = new ()
        {
            gameAnagramDto,
            gameAnagramDto
        },
        GameUserDtos = new()
        {
            gameUserDto,
            gameUserDto
        },
        GameAnagramTypeDto = gameAnagramTypeDto
    };

}