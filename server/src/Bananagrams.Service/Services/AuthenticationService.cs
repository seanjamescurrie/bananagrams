using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Dal.Specifications.Users;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Interfaces;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using BC = BCrypt.Net.BCrypt;

namespace Bananagrams.Service.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IBananagramsDatabase _database; 
    private readonly IMapper _mapper;
    public AuthenticationService(IBananagramsDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public UserDto? Authenticate(string email, string password)
    {
        var user = _database.Get<User>().Where(new UserByEmailSpec(email)).SingleOrDefault();
        
        if (user is null || !BC.Verify(password, user.Password)) return null;

        return _mapper.Map<UserDto>(user);
    }

    
}