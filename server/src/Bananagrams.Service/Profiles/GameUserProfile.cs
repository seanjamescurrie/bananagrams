using AutoMapper;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Dtos.GameUsers;
using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Service.Profiles;

public class GameUserProfile : Profile
{
    public GameUserProfile()
    {
        ConfigureToDomain();
    }

    private void ConfigureToDomain()
    {
        CreateMap<GameUser, GameUserDto>();
        CreateMap<Game, GameDto>();
        CreateMap<User, UserDto>();
        CreateMap<GameUserGameAnagram, GameUserGameAnagramDto>();
    }
}