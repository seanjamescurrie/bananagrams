using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Dtos.DailyWords;
using Bananagrams.Service.Interfaces;

namespace Bananagrams.Service.Services;

public class WordService : IWordService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    
    public WordService(IBananagramsDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public void Create(WordDto word)
    {
        var newWord = _mapper.Map<Word>(word);

        _database.Add(newWord);
        _database.SaveChanges();
    }

}