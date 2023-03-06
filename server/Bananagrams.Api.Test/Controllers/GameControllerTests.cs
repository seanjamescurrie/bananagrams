using System.Net;
using AutoMapper;
using Bananagrams.Api.Controllers;
using Bananagrams.Api.Test.Extensions;
using Bananagrams.Api.ViewModels.Games;
using Bananagrams.Api.ViewModels.GameTypes;
using Bananagrams.Api.ViewModels.GameUserGameAnagrams;
using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameTypes;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace Bananagrams.Api.Test.Controllers;

public class GameControllerTests
{
    private readonly IGameService _gameService;
    private readonly IGameTypeService _gameTypeService;
    private readonly IMapper _mapper;

    public GameControllerTests() =>
        (_gameService, _gameTypeService, _mapper) = (Substitute.For<IGameService>(), Substitute.For<IGameTypeService>(), Substitute.For<IMapper>());

    [Fact]
    public async Task GetGames_WhenGamesExist_MapsAndReturnsOk()
    {
        // Arrange
        var gameDtos = new List<GameDto>
        {
            new()
        };
        var gameViewModels = new List<GameViewModel>
        {
            new()
        };
        var controller = RetrieveController();
    
        _gameService.GetAll("").Returns(gameDtos);
        _mapper.Map<List<GameViewModel>>(gameDtos).Returns(gameViewModels);
    
    
        // Act
        var actionResult = await controller.GetAll("");
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<GameViewModel>, OkObjectResult>();
    
        result.Should().BeSameAs(gameViewModels);
    
        await _gameService.Received(1).GetAll("");
        _mapper.Received(1).Map<List<GameViewModel>>(gameDtos);
    }
    
    [Fact]
    public async Task GetGames_WhenNoneFound_ReturnsNoContent()
    {
        // Arrange
        var controller = RetrieveController();
    
        // Act
        var actionResult = await controller.GetAll("");
        
        // Assert
        actionResult.AssertResult<IList<GameViewModel>, NoContentResult>();
    }
    
    [Fact]
    public async Task GetGameTypes_WhenGameTypesExist_MapsAndReturnsOk()
    {
        // Arrange
        var gameDtos = new List<GameTypeDto>
        {
            new()
        };
        var gameTypeViewModels = new List<GameTypeViewModel>
        {
            new()
        };
        var controller = RetrieveController();
    
        _gameTypeService.GetAll().Returns(gameDtos);
        _mapper.Map<List<GameTypeViewModel>>(gameDtos).Returns(gameTypeViewModels);
    
    
        // Act
        var actionResult = await controller.GetAllGameTypes();
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<GameTypeViewModel>, OkObjectResult>();
    
        result.Should().BeSameAs(gameTypeViewModels);
    
        await _gameTypeService.Received(1).GetAll();
        _mapper.Received(1).Map<List<GameTypeViewModel>>(gameDtos);
    }
    
    [Fact]
    public async Task GetGameTypes_WhenNoneFound_ReturnsNoContent()
    {
        // Arrange
        var controller = RetrieveController();
    
        // Act
        var actionResult = await controller.GetAllGameTypes();
        
        // Assert
        actionResult.AssertResult<IList<GameTypeViewModel>, NoContentResult>();
    }
    
    [Fact]
    public async Task GetGameById_WhenGameExists_MapsAndReturnsOk()
    {
        // Arrange
        const int id = 1;
        var gameDto = new GameDto
        {
            Id = id
        };
        var gameViewModel = new GameDetailViewModel
        {
            Id = id
        };
        
        var controller = RetrieveController();
    
        _gameService.Get(id).Returns(gameDto);
        _mapper.Map<GameDetailViewModel>(gameDto).Returns(gameViewModel);
    
    
        // Act
        var actionResult = await controller.Get(id);
        
        // Assert
        var result = actionResult.AssertObjectResult<GameDetailViewModel, OkObjectResult>();
    
        result.Should().BeSameAs(gameViewModel);
    
        await _gameService.Received(1).Get(id);
        _mapper.Received(1).Map<GameDetailViewModel>(gameDto);
    }
    
    [Fact]
    public async Task GetGameById_WhenNoGameExists_ReturnsNotFound()
    {
        // Arrange
        const int id = 1;
        
        var controller = RetrieveController();
    
        // Act
        var actionResult = await controller.Get(id);
        
        // Assert
        actionResult.AssertResult<GameDetailViewModel, NotFoundResult>();
    }

    [Fact]
    public async Task CreateGame_WhenDataAvailable_MapsAndReturnsCreated()
    {
        // Arrange
        var newCreateGameDto = new CreateGameDto
        {
            Title = "Pure Class Game",
            TotalAnagrams = 5,
            GameAnagramTypeId = 2
        };
        var newCreateGameViewModel = new CreateGameViewModel();
    
        var controller = RetrieveController();
    
        await _gameService.Create(newCreateGameDto);
        _mapper.Map<CreateGameViewModel>(newCreateGameDto).Returns(newCreateGameViewModel);
        
        // Act
        var actionResult = await controller.Create(newCreateGameViewModel);
        
        // Assert
        actionResult.AssertResult<StatusCodeResult>(HttpStatusCode.Created);

        await _gameService.Received(1).Create(newCreateGameDto);
        _mapper.Received(1).Map<CreateGameDto>(newCreateGameViewModel);
    }

    [Fact]
    public async Task UpdateGame_WhenDataExists_MapsAndReturnsOk()
    {
        // Arrange
        var gameId = 1;
        var anagramId = 1;
        var updateGameUserGameAnagramDto = new UpdateGameUserGameAnagramDto
        {
            Attempts = 500,
            DatePlayed = DateTime.UtcNow,
            DateSolved = DateTime.UtcNow
        };
        var updateGameUserGameAnagramViewModel = new UpdateGameViewModel();

        var controller = RetrieveController();

        await _gameService.UpdateGameAnagramForUser(gameId, anagramId, updateGameUserGameAnagramDto);
        _mapper.Map<UpdateGameViewModel>(updateGameUserGameAnagramDto).Returns(updateGameUserGameAnagramViewModel);

        // Act
        var actionResult =
            await controller.UpdateGameUserGameAnagramAttempt(gameId, anagramId, updateGameUserGameAnagramViewModel);

        // Assert
        actionResult.AssertResult<OkResult>();

        await _gameService.Received(1).UpdateGameAnagramForUser(gameId, anagramId, updateGameUserGameAnagramDto);
        _mapper.Received(1).Map<UpdateGameUserGameAnagramDto>(updateGameUserGameAnagramViewModel);
    }

    private GamesController RetrieveController()
    {
        return new GamesController(_gameService, _gameTypeService, _mapper);
    }
}