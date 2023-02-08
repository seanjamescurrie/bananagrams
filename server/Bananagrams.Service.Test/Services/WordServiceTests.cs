using Bananagrams.Service.Dtos.DailyWords;

namespace Bananagrams.Service.Test.Services;

public class WordServiceTests
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    
    public WordServiceTests() =>
        (_database, _mapper) = (Substitute.For<IBananagramsDatabase>(), Substitute.For<IMapper>());

    [Fact]
    public async Task CreateWord_WhenWordPassedIn_CreatesWord()
    {
        // Arrange
        var newWord = new Word()
        {
            Title = "Banana"
        };

        var newWordDto = new WordDto()
        {
            Title = "Banana"
        };

        var service = RetrieveService();

        _mapper.Map<Word>(newWordDto).Returns(newWord);

        // Act
        var result = service.Create(newWordDto);

        // Assert
        await _database.Received(1).SaveChangesAsync();
        _database.Received(1).Add(Arg.Is<Word>(x => x.Title == newWordDto.Title));
    }
    
    private WordService RetrieveService()
    {
        return new WordService(_database, _mapper);
    }
}