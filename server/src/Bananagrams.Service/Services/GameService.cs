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
using Unosquare.EntityFramework.Specification.EFCore.Extensions;

namespace Bananagrams.Service.Services;

public class GameService : IGameService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    private readonly ITropicalFruitApiService _tropicalFruitApiService;

    public GameService(IBananagramsDatabase database, IMapper mapper,
        ITropicalFruitApiService tropicalFruitApiService) =>
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

    public async Task<GameDto> GetDaily(int userId)
    {
        var games = await _mapper.ProjectTo<GameDto>(_database
                .Get<Game>()
                .Where(new GameByDateSpec(DateTime.UtcNow).And(new GameByTypeSpec(1))))
            .ToListAsync();

        if (games != null && games.Any())
        {
            if (games.Any(x => x.GameUsers.Any(x => x.UserId == userId)))
            {
                return games.SingleOrDefault(x => x.GameUsers.Any(x => x.UserId == userId));
            }
            else
            {
                var game = games.FirstOrDefault();
                var newDailyGame = new CreateGameDto
                {
                    GameAnagramTypeId = game.GameAnagramTypeId,
                    PlayerIds = new[] { userId },
                    Title = $"{DateTime.UtcNow.ToShortDateString()}",
                    DailyAnagram = _mapper.Map<GameAnagram>(game.GameAnagrams.FirstOrDefault())
                };
                await Create(newDailyGame);
            }
        }
        else
        {
            var newDailyGame = new CreateGameDto
            {
                PlayerIds = new[] { userId },
                GameAnagramTypeId = 1,
                Title = $"{DateTime.UtcNow.ToShortDateString()}",
                TotalAnagrams = 1
            };
            await Create(newDailyGame);
        }

        return await _mapper.ProjectTo<GameDto>(_database
                .Get<Game>()
                .Where(new GameByDateSpec(DateTime.UtcNow).And(new GameByTypeSpec(1))
                    .And(new GameByUserIdSpec(userId))))
            .SingleOrDefaultAsync();
    }

    public async Task<int> Create(CreateGameDto game)
    {
        var newGame = _mapper.Map<Game>(game);
        newGame.GameAnagrams = new List<GameAnagram>();

        var anagrams = new List<GameAnagram>();

        if (game.DailyAnagram != null)
        {
            anagrams.Add(game.DailyAnagram);
        }
        else
        {
            anagrams = await CreateAnagrams(game);
        }

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

        return newGame.Id;
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

    public async Task<bool> UpdateGameAnagramForUser(int gameId, int anagramId,
        UpdateGameUserGameAnagramDto gameUserGameAnagram)
    {
        var isSolved = false;
        var existingGameUserGameAnagram = await _database.Get<GameUserGameAnagram>()
            .Include(x => x.GameAnagram)
            .ThenInclude(x => x.Word)
            .Include(x => x.GameUser)
            .Where(new GameUserGameAnagramByGameIdSpec(gameId).And(new GameUserGameAnagramAnagramIdSpec(anagramId))
                .And(new GameUserGameAnagramByUserIdSpec(1)))
            .SingleOrDefaultAsync();

        if (existingGameUserGameAnagram == null)
            throw new NotFoundException($"Could not find game with game id: {gameId} and anagram id: {anagramId}");

        gameUserGameAnagram.DatePlayed = DateTime.UtcNow;

        if (gameUserGameAnagram.Attempt == existingGameUserGameAnagram.GameAnagram.Word.Title)
        {
            gameUserGameAnagram.DateSolved = DateTime.UtcNow;
            isSolved = true;
        }

        _mapper.Map(gameUserGameAnagram, existingGameUserGameAnagram);

        await _database.SaveChangesAsync();
        return isSolved;
    }

    private async Task<List<GameAnagram>> CreateAnagrams(CreateGameDto game)
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

        return anagrams.ToList();
    }
}