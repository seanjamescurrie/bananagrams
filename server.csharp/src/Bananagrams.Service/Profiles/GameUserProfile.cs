using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Dtos.GameUsers;
using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Service.Profiles;

[ExcludeFromCodeCoverage]
public class GameUserProfile : Profile
{
    public GameUserProfile()
    {
        ConfigureToDomain();
    }

    private void ConfigureToDomain()
    {
        CreateMap<User, UserDto>();
        CreateMap<GameUserGameAnagram, GameUserGameAnagramDto>();
    }
}