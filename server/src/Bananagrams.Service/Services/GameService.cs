using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Dal.Specifications.Games;
using Bananagrams.Dal.Specifications.GameUserGameAnagrams;
using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.DailyWords;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Exceptions;
using Bananagrams.Service.Extensions;
using Bananagrams.Service.HttpClients;
using Bananagrams.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Bananagrams.Service.Services;

public class GameService : IGameService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    private readonly ITropicalFruitApiService _tropicalFruitApiService;

    public GameService(IBananagramsDatabase database, IMapper mapper, ITropicalFruitApiService tropicalFruitApiService) =>
        (_database, _mapper, _tropicalFruitApiService) = (database, mapper, tropicalFruitApiService);

    public async Task<IList<GameDto>> GetAll(string? searchWord = null)
    {
        var games = await _mapper.ProjectTo<GameDto>(_database
            .Get<Game>()
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

        return game ?? throw new NotFoundException($"Could not find game with id: {id}");
    }

    public async Task Create(CreateGameDto game)
    {
        var newGame = _mapper.Map<Game>(game);
        newGame.GameAnagrams = new List<GameAnagram>();

        var anagrams = await CreateAnagrams(game);
        
        foreach (var a in anagrams)
        {
            a.GameUserGameAnagrams = newGame.GameUsers.Select(x => new GameUserGameAnagram
            {
                GameUser = x,
                GameAnagram = a
            }).ToList();
            
            newGame.GameAnagrams.Add(a);
        }
        
        _database.Add(newGame);
        await _database.SaveChangesAsync();
    }
    
    // public async Task Update(int id, GameDto game)
    // {
    //     var existingGame = _database.Get<Game>().FirstOrDefault( new GameByIdSpec(id));
    //
    //     if (existingGame == null) throw new NotFoundException($"Could not find game with id: {id}");
    //     
    //     _mapper.Map(game, existingGame);
    //     
    //     await _database.SaveChangesAsync();
    // }
    //
    // public async Task Delete(int id)
    // {
    //     var existingGame = _database.Get<Game>().FirstOrDefault( new GameByIdSpec(id));
    //     
    //     if (existingGame == null) throw new NotFoundException($"Could not find game with id: {id}");
    //     
    //     _database.Delete(existingGame);
    //     await _database.SaveChangesAsync();
    // }

    public async Task UpdateGameAnagramForUser(int gameId, int anagramId, UpdateGameUserGameAnagramDto gameUserGameAnagram)
    {
        var existingGameUserGameAnagram = _database.Get<GameUserGameAnagram>().FirstOrDefault(new GameUserGameAnagramByGameIdAnagramIdSpec(gameId, anagramId));
        
        if (existingGameUserGameAnagram == null) throw new NotFoundException($"Could not find game with game id: {gameId} and anagram id: {anagramId}");
        
        _mapper.Map(gameUserGameAnagram, existingGameUserGameAnagram);
        
        await _database.SaveChangesAsync();
    }

    private async Task<GameAnagram[]> CreateAnagrams(CreateGameDto game)
    {
        var anagrams = new GameAnagram[game.TotalAnagrams];

        var fruits = await _tropicalFruitApiService.GetAll("a");
        
        var rnd = new Random();
        var fruitNames = new List<string>();
        for (var i = 1; i <= game.TotalAnagrams; i++)
        {
            var position = rnd.Next(1, fruits.Count);
            fruitNames.Add(fruits.ElementAt(position).Title);
            fruits.Remove(fruits.ElementAt(position));
        }
        
        for (var i = 0; i < fruitNames.Count; i++)
        {
            var word = _mapper.Map<Word>(await _tropicalFruitApiService.Get(fruitNames[i]));
            
            anagrams[i] = new GameAnagram
            {
                AnagramWord = word.Title.Scramble(),
                DateCreated = DateTime.UtcNow,
                GameAnagramTypeId = game.GameAnagramTypeId,
                Order = i + 1,
                Word = word
            };
        }

        return anagrams;
    }
}