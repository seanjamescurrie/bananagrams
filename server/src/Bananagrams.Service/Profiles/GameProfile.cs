using AutoMapper;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos.DailyWords;
using Bananagrams.Service.Dtos.GameAnagrams;
using Bananagrams.Service.Dtos.GameAnagramTypes;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Dtos.GameUsers;

namespace Bananagrams.Service.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        ConfigureDomainToDto();
    }
    
    private void ConfigureDomainToDto()
    {
        CreateMap<Game, GameDto>()
            .ForMember(x => x.TotalAnagrams,opt => opt
                .MapFrom(src => src.GameAnagrams == null || !src.GameAnagrams.Any() ? 0 : src.GameAnagrams.Count));
        CreateMap<GameUser, GameUserDto>();
        CreateMap<GameAnagram, GameAnagramDto>();
        CreateMap<GameUserGameAnagram, GameUserGameAnagramDto>();
        CreateMap<GameAnagramType, GameAnagramTypeDto>();
        CreateMap<Word, WordDto>();
    }
}