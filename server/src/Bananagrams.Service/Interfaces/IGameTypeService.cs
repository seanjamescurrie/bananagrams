using Bananagrams.Service.Dtos.GameTypes;

namespace Bananagrams.Service.Interfaces;

public interface IGameTypeService
{
    Task<List<GameTypeDto>> GetAll();
}