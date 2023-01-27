using Bananagrams.Service.Dtos.GameAnagrams;

namespace Bananagrams.Service.Interfaces;

public interface IGameAnagramService
{
    void Create(GameAnagramDto gameAnagram);
}