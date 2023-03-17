using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bananagrams.Api.ViewModels.GameAnagrams;
using Bananagrams.Api.ViewModels.GameAnagramTypes;
using Bananagrams.Api.ViewModels.Games;
using Bananagrams.Api.ViewModels.GameTypes;
using Bananagrams.Api.ViewModels.GameUserGameAnagrams;
using Bananagrams.Api.ViewModels.Users;
using Bananagrams.Api.ViewModels.Words;
using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.DailyWords;
using Bananagrams.Service.Dtos.GameAnagrams;
using Bananagrams.Service.Dtos.GameAnagramTypes;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameTypes;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Dtos.GameUsers;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Bananagrams.Api.Profiles;

[ExcludeFromCodeCoverage]
public class GameProfile : Profile
{
    public GameProfile()
    {
        ConfigureDtoToView();
        ConfigureViewToDto();
    }

    private void ConfigureDtoToView()
    {
        CreateMap<GameDto, GameViewModel>()
            .ForMember(x => x.DateCreated, opt => opt
                .MapFrom(src => src.DateCreated))
            .ForMember(x => x.GameAnagramType, opt => opt
                .MapFrom(src => src.GameAnagramType))
            .ForMember(d => d.Completed, o => o
                .MapFrom(s => s.GameAnagrams.Any(x => x.GameUserGameAnagrams.Any(y => y.DatePlayed != null || y.DateSolved != null))));
        CreateMap<GameDto, GameDetailViewModel>();
        CreateMap<GameUserDto, UserViewModel>()
            .ForMember(x => x.EmailAddress,opt => opt
                .MapFrom( src => src.User.EmailAddress))
            .ForMember(x => x.FirstName,opt => opt
                .MapFrom( src => src.User.FirstName))
            .ForMember(x => x.LastName,opt => opt
                .MapFrom( src => src.User.LastName))
            .ForMember(x => x.Username,opt => opt
                .MapFrom( src => src.User.Username));
        CreateMap<GameAnagramDto, GameAnagramViewModel>();
        CreateMap<GameUserGameAnagramDto, GameUserGameAnagramViewModel>();
        CreateMap<GameAnagramTypeDto, GameAnagramTypeViewModel>();
        CreateMap<GameTypeDto, GameTypeViewModel>();
        CreateMap<WordDto, WordViewModel>();
    }

    private void ConfigureViewToDto()
    {
        CreateMap<CreateGameViewModel, CreateGameDto>();
    }
}