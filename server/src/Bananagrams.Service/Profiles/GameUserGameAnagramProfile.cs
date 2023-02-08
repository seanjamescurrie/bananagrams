using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos.GameAnagrams;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Dtos.GameUsers;
using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Service.Profiles;

[ExcludeFromCodeCoverage]
public class GameUserGameAnagramProfile : Profile
{
    public GameUserGameAnagramProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }
    
    private void ConfigureDomainToDto()
    {
        CreateMap<GameUserGameAnagram, GameUserGameAnagramDto>();
        CreateMap<GameUser, GameUserDto>();
        CreateMap<GameAnagram, GameAnagramDto>();
        CreateMap<Game, GameDto>();
        CreateMap<User, UserDto>();
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<UpdateGameUserGameAnagramDto, GameUserGameAnagram>()
            .ForMember(x => x.Attempts, opt => opt
                .MapFrom(src => src.Attempts))
            .ForMember(x => x.DatePlayed, opt => opt
                .MapFrom(src => src.DatePlayed))
            .ForMember(x => x.DateSolved, opt => opt
                .MapFrom(src => src.DateSolved));
    }
}