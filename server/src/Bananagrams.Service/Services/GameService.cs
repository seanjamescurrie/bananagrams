using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Dal.Specifications.Games;
using Bananagrams.Dal.Specifications.GameUserGameAnagrams;
using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
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
        
        for (var i = 0; i < game.TotalAnagrams; i++)
        {
            // replace with external API tropicalfruitandveg
            var word = new Word
            {
                Title = "PEACH",
                Description = "Fuzzy fruit",
                ImageLocation = "www.image.com"
            };

            anagrams[i] = new GameAnagram
            {
                AnagramWord = "PECHA",
                DateCreated = DateTime.UtcNow,
                GameAnagramTypeId = game.GameAnagramTypeId,
                Word = word,
                Order = i++
            };

            // anagrams[i].GameUserGameAnagrams = game.PlayerIds.Select(x => new GameUserGameAnagram
            // {
            //     GameAnagram = anagrams[i]
            // }).ToList();
        }

        return anagrams;
    }
}