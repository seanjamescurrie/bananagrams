using Bananagrams.Service.Dtos.GameUserGameAnagrams;

namespace Bananagrams.Service.Interfaces;

public interface IGameUserGameAnagramService
{
    Task Create(GameUserGameAnagramDto gameUserGameAnagram);
    Task Update(int gameId, int anagramId, UpdateGameUserGameAnagramDto gameUserGameAnagram);
}