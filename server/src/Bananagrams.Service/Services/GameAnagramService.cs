using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Service.Dtos.GameAnagrams;
using Bananagrams.Service.Interfaces;

namespace Bananagrams.Service.Services;

public class GameAnagramService : IGameAnagramService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;

    public GameAnagramService(IBananagramsDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public void Create(GameAnagramDto gameAnagram)
    {
        var newGameAnagram = _mapper.Map<GameAnagramDto>(gameAnagram);

        _database.Add(newGameAnagram);
        _database.SaveChanges();
    }
}