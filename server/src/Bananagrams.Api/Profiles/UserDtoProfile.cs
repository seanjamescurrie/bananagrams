using AutoMapper;
using Bananagrams.Api.Models.Games;
using Bananagrams.Api.ViewModels.GameUsers;
using Bananagrams.Api.ViewModels.Users;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUsers;
using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Api.Profiles;

public class UserDtoProfile : Profile
{
    public UserDtoProfile()
    {
        ConfigureToDomain();
    }

    private void ConfigureToDomain()
    {
        CreateMap<UserDto, UserViewModel>();
        CreateMap<UserDto, UserDetailViewModel>();
        CreateMap<GameUserDto, GameUserViewModel>();
        CreateMap<GameDto, GameViewModel>();
    }
}