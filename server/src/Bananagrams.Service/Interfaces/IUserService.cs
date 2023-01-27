using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Service.Interfaces;

public interface IUserService
{
    IList<UserDto> GetAll(string? searchWord = null);
    UserDto Get(int id);
    void Create(UserDto user);
    void Update(int id, UserDto user);
    void Delete(int id);

}