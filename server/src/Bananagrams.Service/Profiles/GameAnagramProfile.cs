using AutoMapper;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.DailyWords;
using Bananagrams.Service.Dtos.GameAnagrams;
using Bananagrams.Service.Dtos.GameAnagramTypes;
using Bananagrams.Service.Dtos.Games;

namespace Bananagrams.Service.Profiles;

public class GameAnagramProfile : Profile
{
    public GameAnagramProfile()
    {
        ConfigureToDomain();
    }

    private void ConfigureToDomain()
    {
        CreateMap<GameAnagram, GameAnagramDto>();
        CreateMap<Game, GameDto>()
            .ForMember(x => x.TotalAnagrams, opt => opt.MapFrom(src => src.GameAnagrams.Count));
        CreateMap<GameAnagramType, GameAnagramTypeDto>();
        CreateMap<Word, WordDto>();
    }
}