using AutoMapper;
using Bananagrams.Api.Models.Games;

using Bananagrams.Service.Dtos.GameUserGameAnagrams;

namespace Bananagrams.Api.Profiles;

public class GameUserGameAnagramDtoProfile : Profile
{
    public GameUserGameAnagramDtoProfile()
    {
        ConfigureViewToDto();
    }

    private void ConfigureViewToDto()
    {
        CreateMap<UpdateGameViewModel, UpdateGameUserGameAnagramDto>()
            .ForMember(x => x.Attempts, opt => opt.MapFrom(src => src.Attempts))
            .ForMember(x => x.DatePlayed, opt => opt.MapFrom(src => src.DatePlayed))
            .ForMember(x => x.DateSolved, opt => opt.MapFrom(src => src.DateSolved));
    }
}