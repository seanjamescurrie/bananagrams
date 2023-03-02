using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Exceptions;
using Bananagrams.Service.HttpClients;
using NSubstitute.ReceivedExtensions;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Bananagrams.Service.Test.Services;

public class GameServiceTests
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    private readonly ITropicalFruitApiService _tropicalFruitApiService;
    
    public GameServiceTests() =>
    (_database, _mapper, _tropicalFruitApiService) = (Substitute.For<IBananagramsDatabase>(), Substitute.For<IMapper>(), Substitute.For<ITropicalFruitApiService>());

    [Fact]
    public async Task GetGames_WhenNoSearchWordAndGamesExist_ReturnsAllGames()
    {
        // Arrange
        var games = new List<Game>();
        for (var i = 1; i <= 10; i++)
        {
            games.Add(new Game { Id = i });
        }

        var gameDtos = games.Select(x => new GameDto { Id = x.Id }).ToList().BuildMock();

        var service = RetrieveService();

        _database.Get<Game>().Returns(games.AsQueryable());
        _mapper.ProjectTo<GameDto>(Arg.Is<IQueryable<Game>>(x => x.Count() == 10)).Returns(gameDtos);

        // Act
        var result = await service.GetAll("");

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GameDto>>();
        result.Should().Contain(gameDtos);
    }
    
    [Fact]
    public async Task GetGames_WhenValidSearchWordPassed_ReturnGame()
    {
        // Arrange
        var games = new List<Game>
        {
            new Game { Id = 1, Title = "Search" }
        };
        var game = games.FirstOrDefault();
        var gameDtos = new List<GameDto>
        {
            new GameDto()
            {
                Id = game.Id,
                Title = game.Title
            }
        }.BuildMock();
        
        var service = RetrieveService();
        
        _database.Get<Game>().Returns(games.AsQueryable());
        _mapper.ProjectTo<GameDto>(Arg.Is<IQueryable<Game>>(x => x.Count() == 1 && x.Contains(game))).Returns(gameDtos);
        
        // Act
        var result =  await service.GetAll("Search");
          
        // Assert
        result.Should().NotBeNull();
        result?.Should().Contain(gameDtos.ElementAt(0));
    }
    
    [Fact]
    public async Task GetGames_WhenInvalidSearchWordPassed_ReturnEmpty()
    {
        // Arrange
        var games = new List<Game>
        {
            new Game { Id = 1, Title = "Search" }
        };
        var game = games.FirstOrDefault();
        var gameDtos = new List<GameDto>().BuildMock();
        
        var service = RetrieveService();
         
        _database.Get<Game>().Returns(games.AsQueryable());
        _mapper.ProjectTo<GameDto>(Arg.Is<IQueryable<Game>>(x => x.Count() == 0)).Returns(gameDtos);
        
        // Act
        var result =  await service.GetAll("Word");
          
        // Assert
        result.Should().NotBeNull();
        result?.Should().Equal(gameDtos);
    }
    
    [Fact]
    public async Task GetGame_WhenValidIdPassed_ReturnDetails()
    {
        // Arrange
        var games = new List<Game>();
        for (var i = 1; i <= 10; i++)
        {
            games.Add(new Game { Id = i });
        }

        var game = games.FirstOrDefault();
        var gameDtos = new List<GameDto>
        {
            new GameDto()
            {
                Id = game.Id
            }
        }.BuildMock();

        var service = RetrieveService();
        
        _database.Get<Game>().Returns(games.AsQueryable());
        _mapper.ProjectTo<GameDto>(Arg.Is<IQueryable<Game>>(x => x.Count() == 1 && x.Contains(game))).Returns(gameDtos);

        // Act
        var result =  await service.Get(game.Id);
          
        // Assert
        result.Should().NotBeNull();
        result?.Should().Be(gameDtos.ElementAt(0));
    }

    [Fact]
    public async Task CreateGame_WhenValidDataPassed_AddToDatabase()
    {
        // Arrange
        var newGameDto = new CreateGameDto()
        {
            GameAnagramTypeId = 2,
            Title = "Game 1",
            PlayerIds = new int[] { 1 },
            TotalAnagrams = 2
        };
        var newGame = new Game
        {
            Title = "Game 1",
            GameUsers = new List<GameUser>
            {
                new GameUser
                {
                    UserId = 1
                }
            },
            GameAnagrams = new List<GameAnagram>
            {
                new()
            }
        };

        var service = RetrieveService();

        _mapper.Map<Game>(newGameDto).Returns(newGame);

        // Act
        var result = service.Create(newGameDto);

        // Assert
        await _database.Received(1).SaveChangesAsync();
        _database.Received(1).Add(Arg.Is<Game>(x => x.Title == newGameDto.Title));
    }

    [Fact]
    public async Task UpdateGame_WhenValidDataPassed_UpdateDatabase()
    {
        // Arrange
        const int gameId = 1;
        const int anagramId = 1;

        var gameUserGameAnagrams = new List<GameUserGameAnagram>
        {
            new GameUserGameAnagram
            {
                Attempts = 0,
                GameAnagram = new GameAnagram
                {
                    Id = anagramId,
                    GameId = gameId
                }
            }
        };

        var updateGameDto = new UpdateGameUserGameAnagramDto()
        {
            Attempts = 2,
            DateSolved = DateTime.UtcNow
        };
        var updateGameUserGameAnagram = new GameUserGameAnagram()
        {
            Attempts = 2,
            DateSolved = DateTime.Now
        };

        var service = RetrieveService();
        
        _database.Get<GameUserGameAnagram>().Returns(gameUserGameAnagrams.AsQueryable());
        _mapper.Map(updateGameDto, updateGameUserGameAnagram);

        // Act
        var result = service.UpdateGameAnagramForUser(gameId, anagramId, updateGameDto);

        // Assert
        await _database.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task UpdateGame_WhenGameNotFound_ThrowException()
    {
        // Arrange
        const int gameId = 1;
        const int anagramId = 1;
    
        var gameUserGameAnagrams = new List<GameUserGameAnagram>();
    
        var updateGameDto = new UpdateGameUserGameAnagramDto()
        {
            Attempts = 2,
            DateSolved = DateTime.UtcNow
        };
    
        var service = RetrieveService();
        
        _database.Get<GameUserGameAnagram>().Returns(gameUserGameAnagrams.AsQueryable());
    
       // Act / Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await service.UpdateGameAnagramForUser(gameId, anagramId, updateGameDto)); 
    }
    
    // [Fact]
    // public async Task CreateGame_WhenValidDataPassed_AddToDatabase()
    // {
    //     // Arrange
    //     // Act
    //     // Assert
    //     
    // }

    private GameService RetrieveService()
    {
        return new GameService(_database, _mapper, _tropicalFruitApiService);
    }
}