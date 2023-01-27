using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.Games;

namespace Bananagrams.Service.Interfaces;

public interface IGameService
{
    Task<List<GameDto>> GetAll(string? searchWord = null);
    Task<GameDto> Get(int id);
    Task Create(CreateGameDto game);
    Task Update(int id, GameDto game);
    Task Delete(int id);
}