using Bananagrams.Service.Dtos.Users;
using BC = BCrypt.Net.BCrypt;

namespace Bananagrams.Service.Test.Services;

public class AuthenticationServiceTests
{
    private readonly IBananagramsDatabase _database; 
    private readonly IMapper _mapper;
    
    public AuthenticationServiceTests() =>
        (_database, _mapper) = (Substitute.For<IBananagramsDatabase>(), Substitute.For<IMapper>());

    [Fact]
    public async Task Authenticate_WhenDetailsValid_MapAndReturnUser()
    {
        // Arrange
        var password = "Password123!";
        var hashedPassword = BC.HashPassword(password);
        var user = new User
        {
            Id = 1,
            EmailAddress = "mail@mail.com",
            Password = hashedPassword
        };
        
        var users = new List<User>
        {
            user
        };

        var userDto = new UserDto
        {
            Id = 1,
            EmailAddress = user.EmailAddress,
            Password = user.Password
        };

        var service = RetrieveService();
        _database.Get<User>().Returns(users.AsQueryable());
        _mapper.Map<UserDto>(user).Returns(userDto);

        // Act
        var result = service.Authenticate(user.EmailAddress, password);

        // Assert
        result.Should().Be(userDto);

        _mapper.Received(1).Map<UserDto>(user);
    }
    
    [Fact]
    public async Task Authenticate_WhenDetailsInValid_ReturnNull()
    {
        // Arrange
        var wrongPassword = "Password123";
        var hashedPassword = BC.HashPassword(wrongPassword);
        var user = new User
        {
            Id = 1,
            EmailAddress = "mail@mail.com",
            Password = hashedPassword
        };
        
        var users = new List<User>
        {
            
        };

        var service = RetrieveService();
        _database.Get<User>().Returns(users.AsQueryable());

        // Act
        var result = service.Authenticate(user.EmailAddress, wrongPassword);

        // Assert
        result.Should().BeNull();

        _mapper.Received(0).Map<UserDto>(user);
    }

    private AuthenticationService RetrieveService()
    {
        return new AuthenticationService(_database, _mapper);
    }
}