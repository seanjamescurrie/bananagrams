using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Exceptions;
using NSubstitute.ReceivedExtensions;

namespace Bananagrams.Service.Test.Services;


public class UserServiceTests
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    
    public UserServiceTests() =>
    (_database, _mapper) = (Substitute.For<IBananagramsDatabase>(), Substitute.For<IMapper>());

    [Fact]
    public async Task GetUsers_WhenNoSearchWordAndUsersExist_ReturnsAllUsers()
    {
        // Arrange
        var users = new List<User>();
        for (var i = 1; i <= 10; i++)
        {
            users.Add(new User { Id = i });
        }

        var userDtos = users.Select(x => new UserDto { Id = x.Id }).ToList().BuildMock();

        var service = RetrieveService();

        _database.Get<User>().Returns(users.AsQueryable());
        _mapper.ProjectTo<UserDto>(Arg.Is<IQueryable<User>>(x => x.Count() == 10)).Returns(userDtos);

        // Act
        var result = await service.GetAll("");

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<UserDto>>();
        result.Should().Contain(userDtos);
    }
    
    [Fact]
    public async Task GetUsers_WhenValidSearchWordPassed_ReturnUser()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, FirstName = "Sean" }
        };
        var user = users.FirstOrDefault();
        var userDtos = new List<UserDto>
        {
            new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName
            }
        }.BuildMock();
        
        var service = RetrieveService();
        
        _database.Get<User>().Returns(users.AsQueryable());
        _mapper.ProjectTo<UserDto>(Arg.Is<IQueryable<User>>(x => x.Count() == 1 && x.Contains(user))).Returns(userDtos);
        
        // Act
        var result =  await service.GetAll("Sean");
          
        // Assert
        result.Should().NotBeNull();
        result?.Should().Contain(userDtos.ElementAt(0));
    }
    
    [Fact]
    public async Task GetUsers_WhenInvalidSearchWordPassed_ReturnEmpty()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, FirstName = "Sean", LastName = "Currie", Username = "seancurrie"}
        };
        var user = users.FirstOrDefault();
        var userDtos = new List<UserDto>().BuildMock();
        
        var service = RetrieveService();
        
        _database.Get<User>().Returns(users.AsQueryable());
        _mapper.ProjectTo<UserDto>(Arg.Is<IQueryable<User>>(x => x.Count() == 0)).Returns(userDtos);
        
        // Act
        var result =  await service.GetAll("David");
          
        // Assert
        result.Should().NotBeNull();
        result?.Should().Equal(userDtos);
    }
    
    [Fact]
    public async Task GetUser_WhenValidIdPassed_ReturnDetails()
    {
        // Arrange
        var users = new List<User>();
        for (var i = 1; i <= 10; i++)
        {
            users.Add(new User { Id = i });
        }

        var user = users.FirstOrDefault();
        var userDtos = new List<UserDto>
        {
            new UserDto()
            {
                Id = user.Id
            }
        }.BuildMock();

        var service = RetrieveService();
        
        _database.Get<User>().Returns(users.AsQueryable());
        _mapper.ProjectTo<UserDto>(Arg.Is<IQueryable<User>>(x => x.Count() == 1 && x.Contains(user))).Returns(userDtos);

        // Act
        var result =  await service.Get(user.Id);
          
        // Assert
        result.Should().NotBeNull();
        result?.Should().Be(userDtos.ElementAt(0));
    }

    [Fact]
    public async Task CreateUser_WhenValidDataPassed_AddToDatabase()
    {
        // Arrange
        var newUserDto = new CreateUserDto()
        {
            FirstName = "Sean",
            Password = "Password123!"
        };
        var newUser = new User
        {
            FirstName = "Sean",
            Password = "Password123!"
        };

        var service = RetrieveService();

        _mapper.Map<User>(newUserDto).Returns(newUser);

        // Act
        var result = service.Create(newUserDto);

        // Assert
        await _database.Received(1).SaveChangesAsync();
        _database.Received(1).Add(Arg.Is<User>(x => x.FirstName == newUserDto.FirstName));
    }

    [Fact]
    public async Task UpdateUser_WhenValidDataPassed_UpdateDatabase()
    {
        // Arrange
        const int id = 1;

        var users = new List<User>
        {
            new User
            {
                Id = id
            }
        };
        
        var updateUserDto = new UpdateUserDto()
        {
            FirstName = "Sean"
        };
        var updateUser = new User()
        {
            FirstName = "Sean"
        };

        var service = RetrieveService();
        
        _database.Get<User>().Returns(users.AsQueryable());
        _mapper.Map(updateUserDto, updateUser);

        // Act
        var result = service.Update(id, updateUserDto);

        // Assert
        await _database.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task UpdateUser_WhenUserNotFound_ThrowException()
    {
        // Arrange
        const int id = 1;
    
        var users = new List<User>();
    
        var updateUserDto = new UpdateUserDto()
        {
            Username = "username"
        };
    
        var service = RetrieveService();
        
        _database.Get<User>().Returns(users.AsQueryable());
    
        // Act / Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await service.Update(id, updateUserDto)); 
    }

    [Fact]
    public async Task UpdateUser_WhenPasswordFieldIsChanged_HashPassword()
    {
        // Arrange
        const int id = 1;
        var updateUser = new User()
        {
            Id = id,
            Password = "NewPassword123!"
        };
    
        var users = new List<User>
        {
            updateUser
        };
    
        var updateUserDto = new UpdateUserDto()
        {
            Password = "NewPassword123!"
        };
    
        var service = RetrieveService();
        
        _database.Get<User>().Returns(users.AsQueryable());
    
        // Act 
        var result = service.Update(id, updateUserDto);
        
        // Assert
        result.Should().NotBeNull();

        _mapper.Received(1);
        await _database.Received(1).SaveChangesAsync();

    }

    [Fact]
    public async Task DeleteUser_WhenValidIdPassed_DeleteFromDatabase()
    {
        // Arrange
        const int id = 1;

        var users = new List<User>
        {
            new User
            {
                Id = id
            }
        };

        var service = RetrieveService();
        
        _database.Get<User>().Returns(users.AsQueryable());

        // Act
        var result = service.Delete(id);

        // Assert
        _database.Received(1).Delete(Arg.Is<User>(x => x.Id == id));
        await _database.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task DeleteUser_WhenUserNotFound_ThrowException()
    {
        // Arrange
        const int id = 1;
    
        var users = new List<User>();
    
        var service = RetrieveService();
        
        _database.Get<User>().Returns(users.AsQueryable());
    
        // Act / Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await service.Delete(id)); 
    }
    
    // [Fact]
    // public async Task CreateGame_WhenValidDataPassed_AddToDatabase()
    // {
    //     // Arrange
    //     // Act
    //     // Assert
    // }

    private UserService RetrieveService()
    {
        return new UserService(_database, _mapper);
    }
}