using System.Net;
using System.Net.Http.Json;
using Bananagrams.Api.Integration.Test.Base;
using Bananagrams.Api.Integration.Test.Models;
using Bananagrams.Api.Integration.Test.TestUtilities;
using Bananagrams.Api.ViewModels.Games;
using Bananagrams.Api.ViewModels.GameTypes;
using FluentAssertions;
using Xunit.Abstractions;

namespace Bananagrams.Api.Integration.Test.Controllers;

[Collection("Integration")]
public class GameControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public GameControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationFixture.Host.CreateClient();
    }

    [Fact]
    public async Task GetAll_WhenGamesPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/games/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<GameViewModel[]>());
    }

    [Fact]
    public async Task GetAllGameTypes_WhenGameTypesPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/games/types");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<GameTypeViewModel[]>());
    }
    
    [Fact]
    public async Task Get_WhenGameFound_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/games/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        var gameDetail = value.VerifyDeSerialize<GameDetailViewModel>();

        gameDetail.Title.Should().Be("Test");
        _testOutputHelper.WriteLine(value);
    }

    [Fact]
    public async Task Create_WhenGameCreated_ReturnsCreated()
    {
        var newGame = new CreateGameViewModel
        {
            GameAnagramTypeId = 2,
            PlayerIds = new int [] { 1 },
            Title = "Game Title",
            TotalAnagrams = 1
        };
        
        var response = await _httpClient.PostAsJsonAsync("/games/", newGame);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var value = await response.Content.ReadAsStringAsync();
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task Create_WhenNewGameDetailsInvalid_ReturnsValidationErrors()
    {
        var newGame = new CreateGameViewModel();
        
        var response = await _httpClient.PostAsJsonAsync("/games/", newGame);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("TotalAnagrams", "Please select the number of anagrams for the game");
        result.Errors.CheckIfErrorPresent("PlayerIds", "The PlayerIds field is required.");
        result.Errors.CheckIfErrorPresent("Title", "The Title field is required.");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task UpdateGameUserGameAnagramAttempt_WhenGameDetailsInvalid_ReturnsValidationErrors()
    {
        const int id = 1;
        const int anagramId = 1;
        var updateGame = new UpdateGameViewModel();
        
        var response = await _httpClient.PutAsJsonAsync($"/games/{id}/Attempt/{anagramId}", updateGame);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Attempts", "Attempts must not be empty.");
        result.Errors.CheckIfErrorPresent("DatePlayed", "Date Played must not be empty.");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task UpdateGameUserGameAnagramAttempt_WhenGameDetailsValid_ReturnsOk()
    {
        const int id = 1;
        const int anagramId = 1;
        var updateGame = new UpdateGameViewModel
        {
            Attempts = 2,
            DatePlayed = DateTime.UtcNow,
            DateSolved = DateTime.UtcNow
        };
        
        var response = await _httpClient.PutAsJsonAsync($"/games/{id}/Attempt/{anagramId}", updateGame);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var value = await response.Content.ReadAsStringAsync();
        
        _testOutputHelper.WriteLine(value);
    }
}