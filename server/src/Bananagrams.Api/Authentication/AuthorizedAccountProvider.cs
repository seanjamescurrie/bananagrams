using System.Security.Claims;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Interfaces;

namespace Bananagrams.Api.Authentication;

public class AuthorizedAccountProvider : IAuthorizedAccountProvider
{
    private UserDto? _user;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthorizedAccountProvider(IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _contextAccessor = contextAccessor;
    }
    
    public async Task<UserDto> GetLoggedInAccount()
    {
        if (_user is not null) return _user;

        var identifier = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(identifier)) return null;

        _user = await _userService.Get(int.Parse(identifier));

        return _user;
    }
}

public interface IAuthorizedAccountProvider
{
    Task<UserDto> GetLoggedInAccount();
}