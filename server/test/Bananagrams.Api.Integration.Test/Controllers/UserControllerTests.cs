using System.Net;
using System.Net.Http.Json;
using Bananagrams.Api.Integration.Test.Base;
using Bananagrams.Api.Integration.Test.Models;
using Bananagrams.Api.Integration.Test.TestUtilities;
using Bananagrams.Api.ViewModels.Games;
using Bananagrams.Api.ViewModels.Users;
using FluentAssertions;
using Xunit.Abstractions;

namespace Bananagrams.Api.Integration.Test.Controllers;

[Collection("Integration")]
public class UserControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public UserControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationFixture.Host.CreateClient();
    }

    [Fact]
    public async Task GetAll_WhenUsersPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/users/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<UserViewModel[]>());
    }
    
    [Fact]
    public async Task Get_WhenUserFound_ReturnsOk()
    {
        const int id = 1;
        var response = await _httpClient.GetAsync($"/users/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        var gameDetail = value.VerifyDeSerialize<UserDetailViewModel>();

        gameDetail.FirstName.Should().Be("Sean");
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task Create_WhenGameCreated_ReturnsCreated()
    {
        var newUser = new CreateUserViewModel
        {
            EmailAddress = "mail@mail.com",
            FirstName = "David",
            LastName = "Currie",
            Username = "davidcurrie",
            Password = "Password123!"
        };
        
        var response = await _httpClient.PostAsJsonAsync("/users/", newUser);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var value = await response.Content.ReadAsStringAsync();
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task Create_WhenNewUserDetailsInvalid_ReturnsValidationErrors()
    {
        var newUser = new CreateUserViewModel();
        
        var response = await _httpClient.PostAsJsonAsync("/users/", newUser);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("FirstName", "First name must be entered");
        result.Errors.CheckIfErrorPresent("LastName", "Last name must be entered");
        result.Errors.CheckIfErrorPresent("Username", "Username must be entered");
        result.Errors.CheckIfErrorPresent("EmailAddress", "Email must be entered");
        result.Errors.CheckIfErrorPresent("Password", "Password must be entered");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task UpdateUser_WhenNoUserDetails_ReturnsValidationErrors()
    {
        const int id = 1;
        var updateUser = new UpdateUserViewModel();
        
        var response = await _httpClient.PutAsJsonAsync($"/users/{id}", updateUser);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("NoValue", "At least one value required");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task UpdateUser_WhenUserDetailsInvalid_ReturnsValidationErrors()
    {
        const int id = 1;
        var updateUser = new UpdateUserViewModel
        {
            FirstName = "S",
            LastName = "C",
            Password = "P",
            Username = "U"
        };
        
        var response = await _httpClient.PutAsJsonAsync($"/users/{id}", updateUser);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("FirstName", "First name must be between 2 and 100 characters in length");
        result.Errors.CheckIfErrorPresent("LastName", "Last name must be between 2 and 100 characters in length");
        result.Errors.CheckIfErrorPresent("Password", "Password must be between 8 and 30 characters in length");
        result.Errors.CheckIfErrorPresent("Username", "Username must be between 2 and 30 characters in length");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task UpdateUser_WhenUserDetailsValid_ReturnsOk()
    {
        const int id = 1;
        var updateUser = new UpdateUserViewModel
        {
            FirstName = "Sean",
            LastName = "Currie",
            Password = "Password123!",
            Username = "Username"
        };
        
        var response = await _httpClient.PutAsJsonAsync($"/users/{id}", updateUser);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value);
    }
}