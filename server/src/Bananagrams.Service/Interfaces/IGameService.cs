using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;

namespace Bananagrams.Service.Interfaces;

public interface IGameService
{
    Task<IList<GameDto>> GetAll(string? searchWord = null);
    Task<GameDto> Get(int id);
    Task Create(CreateGameDto game);
    Task UpdateGameAnagramForUser(int gameId, int anagramId, UpdateGameUserGameAnagramDto gameUserGameAnagram);
}