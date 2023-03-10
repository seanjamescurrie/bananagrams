using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bananagrams.Api.ViewModels.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;

namespace Bananagrams.Api.Profiles;

[ExcludeFromCodeCoverage]
public class GameUserGameAnagramProfile : Profile
{
    public GameUserGameAnagramProfile()
    {
        ConfigureViewToDto();
    }

    private void ConfigureViewToDto()
    {
        CreateMap<UpdateGameViewModel, UpdateGameUserGameAnagramDto>()
            .ForMember(x => x.Attempts, opt => opt.MapFrom(src => src.Attempts))
            .ForMember(x => x.Attempt, o => o.MapFrom(src => src.Attempt));
    }
}