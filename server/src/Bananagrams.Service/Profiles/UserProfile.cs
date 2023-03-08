using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUsers;
using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Service.Profiles;

[ExcludeFromCodeCoverage]
public class UserProfile : Profile
{
    public UserProfile()
    {
        ConfigureDomainToDto();
        ConfigureDtoToDomain();
    }

    private void ConfigureDomainToDto()
    {
        CreateMap<User, UserDto>()
            .ForMember(d => d.GameUsers, o => o.Ignore());
        CreateMap<User, UpdateUserDto>();
    }

    private void ConfigureDtoToDomain()
    {
        CreateMap<UserDto, User>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}