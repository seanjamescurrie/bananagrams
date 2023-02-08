using Bananagrams.Service.Dtos.DailyWords;

namespace Bananagrams.Service.Interfaces;

public interface IWordService
{
    Task Create(WordDto word);
}