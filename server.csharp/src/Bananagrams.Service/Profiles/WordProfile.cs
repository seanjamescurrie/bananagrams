using AutoMapper;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos.DailyWords;

namespace Bananagrams.Service.Profiles;

public class WordProfile : Profile
{
    public WordProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
        ConfigureJsonToDto();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<Word, WordDto>();
    }
    
    private void ConfigureDtoToDomain()
    {
        CreateMap<WordDto, Word>();
    }

    private void ConfigureJsonToDto()
    {
        CreateMap<JsonResultDto, WordDto>()
            .ForMember(s => s.Description, o => o.MapFrom(d => d.Description))
            .ForMember(s => s.Title, o => o.MapFrom(d => d.Tfvname))
            .ForMember(s => s.ImageLocation, o => o.MapFrom(d => d.ImageUrl));
    }
}