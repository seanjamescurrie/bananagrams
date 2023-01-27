using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Dal.Specifications.Users;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Interfaces;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Bananagrams.Service.Services;

public class UserService : IUserService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;

    public UserService(IBananagramsDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public IList<UserDto> GetAll(string? searchWord = null)
    {
        var users = _mapper.ProjectTo<UserDto>(_database.Get<User>()
            .Where( new UserByFirstNameSpec(searchWord).Or( new UserByLastNameSpec(searchWord) ).Or( new UserByUsernameSpec(searchWord) ) )
            )
            .ToList();
        
        return users;
    }

    public UserDto Get(int id)
    {
        var viewModel = _mapper.ProjectTo<UserDto>(_database.Get<User>()
            .Where(new UserByIdSpec(id)))
            .SingleOrDefault();

        return viewModel;
    }

    public void Create(UserDto user)
    {
        var newUser = _mapper.Map<User>(user);

        _database.Add(newUser);
        _database.SaveChanges();
    }

    public void Update(int id, UserDto user)
    {
        var existingUser = _database.Get<User>().FirstOrDefault( new UserByIdSpec(id));
        _mapper.Map(user, existingUser);
        
        _database.SaveChanges();
    }

    public void Delete(int id)
    {
        var existingUser = _database.Get<User>().FirstOrDefault( new UserByIdSpec(id));
        
        _database.Delete(existingUser);
        _database.SaveChanges();
    }
}