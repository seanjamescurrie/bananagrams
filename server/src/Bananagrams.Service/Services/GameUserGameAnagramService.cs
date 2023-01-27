using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Dal.Specifications.GameUserGameAnagrams;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Interfaces;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Bananagrams.Service.Services;

public class GameUserGameAnagramService : IGameUserGameAnagramService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    
    public GameUserGameAnagramService(IBananagramsDatabase database, IMapper mapper) =>
    (_database, _mapper) = (database, mapper);

    public async Task Create(GameUserGameAnagramDto gameUserGameAnagram)
    {
        var newGameUserGameAnagramDto = _mapper.Map<GameUserGameAnagram>(gameUserGameAnagram);

        _database.Add(newGameUserGameAnagramDto);
        await _database.SaveChangesAsync();
    }

    public async Task Update(int gameId, int anagramId, UpdateGameUserGameAnagramDto gameUserGameAnagram)
    {
        var existingGameUserGameAnagram = _database.Get<GameUserGameAnagram>().FirstOrDefault(new GameUserGameAnagramByGameIdAnagramIdSpec(gameId, anagramId));
        _mapper.Map(gameUserGameAnagram, existingGameUserGameAnagram);
        
        await _database.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var existingGameUserGameAnagram = _database.Get<GameUserGameAnagram>().FirstOrDefault(new GameUserGameAnagramByIdSpec(id));
        
        _database.Delete(existingGameUserGameAnagram);
        await _database.SaveChangesAsync();
    }
}