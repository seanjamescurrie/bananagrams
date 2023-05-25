using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Service.Interfaces;

public interface IUserService
{
    Task<IList<UserDto>> GetAll(string? searchWord = null);
    Task<UserDto> Get(int id);
    Task Create(CreateUserDto user);
    Task Update(int id, UpdateUserDto user);
    Task Delete(int id);

}