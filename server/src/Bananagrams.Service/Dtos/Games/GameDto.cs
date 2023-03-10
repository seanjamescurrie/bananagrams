using Bananagrams.Service.Dtos.GameAnagrams;
using Bananagrams.Service.Dtos.GameAnagramTypes;
using Bananagrams.Service.Dtos.GameUsers;

namespace Bananagrams.Service.Dtos.Games;

public class GameDto
{
    public int Id { get; set; }
    public string? Title { get; set; }    
    public int TotalAnagrams { get; set; }
    public int GameAnagramTypeId { get; set; }
    public DateTime DateCreated { get; set; }

    public GameAnagramTypeDto GameAnagramType { get; set; }
    public GameUserDto[]? GameUsers { get; set; }
    public GameAnagramDto[]? GameAnagrams { get; set; }
}