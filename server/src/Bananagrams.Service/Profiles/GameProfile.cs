using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.DailyWords;
using Bananagrams.Service.Dtos.GameAnagrams;
using Bananagrams.Service.Dtos.GameAnagramTypes;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameTypes;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Dtos.GameUsers;
using Bananagrams.Service.Dtos.Users;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Bananagrams.Service.Profiles;

[ExcludeFromCodeCoverage]
public class GameProfile : Profile
{
    public GameProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }
    
    private void ConfigureDomainToDto()
    {
        CreateMap<GameUser, GameUserDto>()
            .ForMember(d => d.TotalAttempts, o => o
                .MapFrom(s => s.GameUserGameAnagrams.Select(x => x.Attempts).Sum()))
            .ForMember(d => d.TotalSolved, o => o
                .MapFrom(s => s.GameUserGameAnagrams.Count(x => x.DateSolved != null)));
        CreateMap<GameAnagram, GameAnagramDto>();
        CreateMap<GameUserGameAnagram, GameUserGameAnagramDto>();
        CreateMap<GameAnagramType, GameAnagramTypeDto>();
        CreateMap<GameAnagramType, GameTypeDto>().ForMember(d => d.Description, o => o
            .MapFrom(x => x.Title));
        CreateMap<Word, WordDto>();
        CreateMap<Game, GameDto>()
            .ForMember(d => d.TotalAnagrams,o => o
                .MapFrom(s => s.GameAnagrams.Any() ? s.GameAnagrams.Count : 0))
            .ForMember(d => d.GameAnagramType, o => o
                .MapFrom( s => s.GameAnagrams.FirstOrDefault().GameAnagramType));
    }
    
    private void ConfigureDtoToDomain()
    {
        
        CreateMap<GameDto, Game>();
        CreateMap<int, GameUser>()
            .ForMember(d => d.UserId, o => o.MapFrom(s => s));
        CreateMap<int, GameAnagramType>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s));
        CreateMap<CreateGameDto, Game>()
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
            .ForMember(d => d.GameUsers, o => o.MapFrom(s => s.PlayerIds))
            .ForMember(d => d.DateCreated, o => o.MapFrom(s => DateTime.UtcNow));
        CreateMap<GameUserDto, GameUser>();
        CreateMap<UserDto, User>();
        CreateMap<GameAnagramDto, GameAnagram>();
        CreateMap<GameUserGameAnagramDto, GameUserGameAnagram>();
        CreateMap<GameAnagramTypeDto, GameAnagramType>();
        CreateMap<WordDto, Word>();
    }
}