using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Dal.Specifications.Games;
using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Exceptions;
using Bananagrams.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Bananagrams.Service.Services;

public class GameService : IGameService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;

    public GameService(IBananagramsDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public async Task<List<GameDto>> GetAll(string? searchWord = null)
    {
        var games = await _mapper.ProjectTo<GameDto>(_database.Get<Game>()
            .Where(new GameByTitleSpec(searchWord)))
            .ToListAsync();

        return games;
    }

    public async Task<GameDto> Get(int id)
    {
        var game = await _mapper.ProjectTo<GameDto>(_database
            .Get<Game>()
            .Where(new GameByIdSpec(id)))
            .SingleOrDefaultAsync();

        return game ?? throw new NotFoundException($"Could not find game of id: {id}");;
    }

    public async Task Create(CreateGameDto game)
    {
        var newGame = _mapper.Map<Game>(game);

        _database.Add(newGame);
        await _database.SaveChangesAsync();
    }

    public async Task Update(int id, GameDto game)
    {
        var existingGame = _database.Get<Game>().FirstOrDefault( new GameByIdSpec(id));
        _mapper.Map(game, existingGame);
        
        await _database.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var existingGame = _database.Get<Game>().FirstOrDefault( new GameByIdSpec(id));
        
        _database.Delete(existingGame);
        await _database.SaveChangesAsync();
    }
}