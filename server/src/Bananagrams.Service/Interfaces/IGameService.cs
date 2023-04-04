using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;

namespace Bananagrams.Service.Interfaces;

public interface IGameService
{
    Task<IList<GameDto>> GetAll(int userId, string? searchWord = null);
    Task<GameDto> Get(int id);
    Task<GameDto> GetDaily(int userId);
    Task<int> Create(CreateGameDto game);
    Task<bool> UpdateGameAnagramForUser(int gameId, int anagramId, int userId, UpdateGameUserGameAnagramDto gameUserGameAnagram);
}