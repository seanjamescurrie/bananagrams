using AutoMapper;
using Bananagrams.Api.Controllers;
using Bananagrams.Api.Test.Extensions;
using Bananagrams.Api.ViewModels.Authentication;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Bananagrams.Api.Test.Controllers;

public class AuthenticationControllerTests
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;
    
    public AuthenticationControllerTests() =>
    (_mapper, _authenticationService) = (Substitute.For<IMapper>(), Substitute.For<IAuthenticationService>());

    [Fact]
    public async Task Authenticate_WhenDataIsCorrect_AuthenticateUser()
    {
        // Arrange
        var authenticationRequestViewModel = new AuthenticationRequestViewModel
        {
            Email = "sean@mail.com",
            Password = "Password123!"
        };
        var userDto = new UserDto
        {
            EmailAddress = authenticationRequestViewModel.Email,
            Password = authenticationRequestViewModel.Password
        };
        
        var controller = RetrieveController();

        _authenticationService
            .Authenticate(authenticationRequestViewModel.Email, authenticationRequestViewModel.Password)
            .Returns(userDto);
        
        // Act
        var actionResult = controller.Authenticate(authenticationRequestViewModel);

        // Assert
        var result = actionResult.AssertObjectResult<AuthenticationResultViewModel, OkObjectResult>();

        result.Should().NotBeNull();
        
        _authenticationService.Received(1).Authenticate(authenticationRequestViewModel.Email, authenticationRequestViewModel.Password);
    }
    
    [Fact]
    public async Task Authenticate_WhenUserIsNotAuthenticated_ReturnUnauthorized()
    {
        // Arrange
        var authenticationRequestViewModel = new AuthenticationRequestViewModel
        {
            Email = "sean@mail.com",
            Password = "Password123!"
        };
        var userDto = new UserDto
        {
            EmailAddress = authenticationRequestViewModel.Email,
            Password = authenticationRequestViewModel.Password
        };

        userDto = null;
        
        var controller = RetrieveController();

        _authenticationService
            .Authenticate(authenticationRequestViewModel.Email, authenticationRequestViewModel.Password)
            .Returns(userDto);
        
        // Act
        var actionResult = controller.Authenticate(authenticationRequestViewModel);

        // Assert
        actionResult.AssertResult<AuthenticationResultViewModel, UnauthorizedResult>();
        
    }
    
    private AuthenticationController RetrieveController()
    {
        return new AuthenticationController(_mapper, _authenticationService);
    }
}