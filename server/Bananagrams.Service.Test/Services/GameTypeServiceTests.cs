using Bananagrams.Service.Dtos.GameAnagramTypes;
using Bananagrams.Service.Dtos.GameTypes;

namespace Bananagrams.Service.Test.Services;

public class GameTypeServiceTests
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;
    
    public GameTypeServiceTests() =>
        (_database, _mapper) = (Substitute.For<IBananagramsDatabase>(), Substitute.For<IMapper>());

    [Fact]
    public async Task GetGameTypes_WhenExists_ReturnGameTypes()
    {
        // Arrange
        var gameTypes = new List<GameAnagramType>
        {
            new GameAnagramType() { Title = "Daily" },
        };
        var gameTypeDtos = new List<GameTypeDto>
        {
            new GameTypeDto() { Description = "Daily" },
        }.BuildMock();

        var service = RetrieveService();

        _database.Get<GameAnagramType>().Returns(gameTypes.AsQueryable());
        _mapper.ProjectTo<GameTypeDto>(Arg.Is<IQueryable<GameAnagramType>>(x => x.Count() == 1)).Returns(gameTypeDtos);

        // Act
        var result = await service.GetAll();

        // Asssert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GameTypeDto>>();
        result.Should().Contain(gameTypeDtos);
    }
    
    [Fact]
    public async Task GetGameTypes_WhenNoneExists_ReturnEmpty()
    {
        // Arrange
        var gameTypes = new List<GameAnagramType>();
        
        var gameTypeDtos = new List<GameTypeDto>().BuildMock();

        var service = RetrieveService();

        _database.Get<GameAnagramType>().Returns(gameTypes.AsQueryable());
        _mapper.ProjectTo<GameTypeDto>(Arg.Is<IQueryable<GameAnagramType>>(x => x.Count() == 0)).Returns(gameTypeDtos);

        // Act
        var result = await service.GetAll();

        // Asssert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GameTypeDto>>();
        result.Should().Equal(gameTypeDtos);
    }

    public GameTypeService RetrieveService()
    {
        return new GameTypeService(_database, _mapper);
    }

}