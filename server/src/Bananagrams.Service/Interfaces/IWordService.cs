using Bananagrams.Service.Dtos.DailyWords;

namespace Bananagrams.Service.Interfaces;

public interface IWordService
{
    void Create(WordDto word);
}