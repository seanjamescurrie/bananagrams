using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos.GameAnagramTypes;
using Bananagrams.Service.Interfaces;

namespace Bananagrams.Service.Services;

public class GameAnagramTypeService : IGameAnagramTypeService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    
    public GameAnagramTypeService(IBananagramsDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public async Task Create(GameAnagramTypeDto gameAnagramType)
    {
        var model = _mapper.Map<GameAnagramType>(gameAnagramType);

        _database.Add(model);
        await _database.SaveChangesAsync();
    }
    
}