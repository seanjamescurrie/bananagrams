using Bananagrams.Service.Dtos.GameAnagrams;
using Bananagrams.Service.Dtos.GameUsers;

namespace Bananagrams.Service.Dtos.Games;

public class GameDto
{
    public int Id { get; set; }
    public string? Title { get; set; }    
    public int TotalAnagrams { get; set; }
    public DateTime DateCreated { get; set; }
    public GameUserDto[]? GameUsers { get; set; }
    public GameAnagramDto[]? GameAnagrams { get; set; }
}