using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Service.Interfaces;

public interface IAuthenticationService
{
    UserDto? Authenticate(string email, string password);
}