using System.Net;
using AutoMapper;
using Bananagrams.Api.ViewModels.Users;
using Bananagrams.Api.Controllers;
using Bananagrams.Api.Test.Extensions;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Bananagrams.Api.Test.Controllers;

public class UserControllerTests
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserControllerTests() =>
        (_userService, _mapper) = (Substitute.For<IUserService>(), Substitute.For<IMapper>());

    [Fact]
    public async Task GetUsers_WhenUsersExist_MapsAndReturnsOk()
    {
        // Arrange
        var userDtos = new List<UserDto> { new() };
        var userViewModels = new List<UserViewModel> { new() };
    
        var controller = RetrieveController();
    
        _userService.GetAll("").Returns(userDtos);
        _mapper.Map<List<UserViewModel>>(userDtos).Returns(userViewModels);
        
        // Act
        var actionResult = await controller.GetAll("");
        
        // Assert
        var result = actionResult.AssertObjectResult<IList<UserViewModel>, OkObjectResult>();
    
        result.Should().BeSameAs(userViewModels);
    
        await _userService.Received(1).GetAll("");
        _mapper.Received(1).Map<List<UserViewModel>>(userDtos);
    }
    
    [Fact]
    public async Task GetUsers_WhenUsersDontExist_ReturnsNoContent()
    {
        // Arrange
        var controller = RetrieveController();

        // Act
        var actionResult = await controller.GetAll("");

        // Assert
         actionResult.AssertResult<IList<UserViewModel>, NoContentResult>();
    }
    
    [Fact]
    public async Task GetUserById_WhenUsersExist_MapsAndReturnsOk()
    {
        // Arrange
        const int id = 1;
        var userDto = new UserDto
        {
            FirstName = "Sean",
            LastName = "Currie",
            Username = "seancurrie"
        };
        var userViewModel = new UserDetailViewModel
        {
            FirstName = "Sean",
            LastName = "Currie",
            Username = "seancurrie"
        };
        var controller = RetrieveController();

        _userService.Get(id).Returns(userDto);
        _mapper.Map<UserDetailViewModel>(userDto).Returns(userViewModel);

        // Act
        var actionResult = await controller.Get(id);

        // Assert
        var result = actionResult.AssertObjectResult<UserDetailViewModel, OkObjectResult>();

        result.Should().NotBeNull();
        result.Should().BeSameAs(userViewModel);

        await _userService.Received(1).Get(id);
        _mapper.Received(1).Map<UserDetailViewModel>(userDto);
    }
    
    [Fact]
    public async Task GetUserById_WhenNoUserExists_ReturnsNotFound()
    {
        // Arrange
        const int id = 1;

        var controller = RetrieveController();

        // Act
        var actionResult = await controller.Get(id);

        // Assert
        actionResult.AssertResult<UserDetailViewModel, NotFoundResult>();
    }
    
    [Fact]
    public async Task CreateUser_WhenDataAvailable_MapsAndReturnsCreated()
    {
        // Arrange
        var newCreateUserDto = new CreateUserDto
        {
            FirstName = "Sean"
        };
        var newCreateUserViewModel = new CreateUserViewModel
        {
            FirstName = "Sean"
        };
    
        var controller = RetrieveController();
    
        await _userService.Create(newCreateUserDto);
        _mapper.Map<CreateUserViewModel>(newCreateUserDto).Returns(newCreateUserViewModel);
        
        // Act
        var actionResult = await controller.Create(newCreateUserViewModel);
        
        // Assert
        actionResult.AssertResult<StatusCodeResult>(HttpStatusCode.Created);
    
        await _userService.Received(1).Create(newCreateUserDto);
        _mapper.Received(1).Map<CreateUserDto>(newCreateUserViewModel);
    }
    
    [Fact]
    public async Task UpdateUser_WhenDataExists_MapsAndReturnsOk()
    {
        // Arrange
        var id = 1;
        var updateUserDto = new UpdateUserDto
        {
            FirstName = "Finbar"
        };
        var updateUserViewModel = new UpdateUserViewModel();
    
        var controller = RetrieveController();
    
        await _userService.Update(id, updateUserDto);
        _mapper.Map<UpdateUserViewModel>(updateUserDto).Returns(updateUserViewModel);
    
        // Act
        var actionResult = await controller.Update(id, updateUserViewModel);
    
        // Assert
        actionResult.AssertResult<OkResult>();
    
        await _userService.Received(1).Update(id, updateUserDto);
        _mapper.Received(1).Map<UpdateUserDto>(updateUserViewModel);
    }
    
    [Fact]
    public async Task DeleteUser_WhenUserExists_MapsAndReturnsOk()
    {
        // Arrange
        var id = 1;
    
        var controller = RetrieveController();

        // Act
        var actionResult = await controller.Delete(id);
    
        // Assert
        actionResult.AssertResult<OkResult>();
    
        await _userService.Received(1).Delete(id);
    }

    private UsersController RetrieveController()
    {
        return new UsersController(_userService, _mapper);
    }
}