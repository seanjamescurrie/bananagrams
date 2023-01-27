using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Service.Dtos.GameUsers;
using Bananagrams.Service.Interfaces;

namespace Bananagrams.Service.Services;

public class GameUserService : IGameUserService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    
    public GameUserService(IBananagramsDatabase database, IMapper mapper) =>
    (_database, _mapper) = (database, mapper);

    public void Create(GameUserDto gameUser)
    {
        var newGameUser = _mapper.Map<GameUserDto>(gameUser);

        _database.Add(newGameUser);
        _database.SaveChanges();
    }
}