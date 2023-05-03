using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Dal.Specifications.Users;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Exceptions;
using Bananagrams.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Bananagrams.Service.Services;

public class UserService : IUserService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;

    public UserService(IBananagramsDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public async Task<IList<UserDto>> GetAll(string? searchWord = null)
    {
        var users = await _mapper.ProjectTo<UserDto>(_database.Get<User>()
            .Where( new UserByFirstNameSpec(searchWord).Or( new UserByLastNameSpec(searchWord) ).Or( new UserByUsernameSpec(searchWord) ) )
            )
            .ToListAsync();
        
        return users;
    }

    public async Task<UserDto> Get(int id)
    {
        var user = await _mapper.ProjectTo<UserDto>(_database.Get<User>()
            .Where(new UserByIdSpec(id)))
            .SingleOrDefaultAsync();

        return user ?? throw new NotFoundException($"Could not find user with id: {id}");
    }

    public async Task Create(CreateUserDto user)
    {
        var newUser = _mapper.Map<User>(user);

        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

        _database.Add(newUser);
        await _database.SaveChangesAsync();
    }

    public async Task Update(int id, UpdateUserDto user)
    {
        var existingUser = _database.Get<User>().FirstOrDefault( new UserByIdSpec(id));
        
        if (existingUser == null) throw new NotFoundException($"Could not find user with id: {id}");

        if (user.Password != null)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        }
        
        _mapper.Map(user, existingUser);
        
        await _database.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var existingUser = _database.Get<User>().FirstOrDefault( new UserByIdSpec(id));
        if (existingUser == null) throw new NotFoundException($"Could not find user with id: {id}");

        _database.Delete(existingUser);
        await _database.SaveChangesAsync();
    }
}