using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bananagrams.Api.ViewModels.Games;
using Bananagrams.Api.ViewModels.GameUsers;
using Bananagrams.Api.ViewModels.Users;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUsers;
using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Api.Profiles;

[ExcludeFromCodeCoverage]
public class UserProfile : Profile
{
    public UserProfile()
    {
        ConfigureDtoToViewModel();
        ConfigureViewModelToDto();
    }

    private void ConfigureDtoToViewModel()
    {
        CreateMap<UserDto, UserViewModel>();
        CreateMap<UserDto, UserDetailViewModel>();
        CreateMap<GameUserDto, GameUserViewModel>();
        CreateMap<GameDto, GameViewModel>();
        CreateMap<UserDto, UpdateUserViewModel>()
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
            .ForMember(d => d.Password, o => o.MapFrom(s => s.Password))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.Username));
    }

    private void ConfigureViewModelToDto()
    {
        CreateMap<CreateUserViewModel, CreateUserDto>();
        CreateMap<UpdateUserViewModel, UpdateUserDto>();
        // .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
        // .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
        // .ForMember(d => d.Password, o => o.MapFrom(s => s.Password))
        // .ForMember(d => d.Username, o => o.MapFrom(s => s.Username));
    }
}