using AutoMapper;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUsers;
using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Service.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        ConfigureDomainToProfile();
    }

    private void ConfigureDomainToProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<GameUser, GameUserDto>();
        CreateMap<Game, GameDto>();
    }
}