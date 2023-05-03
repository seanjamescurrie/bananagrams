using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Dtos.Users;

namespace Bananagrams.Service.Dtos.GameUsers;

public class GameUserDto
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public int TotalAttempts { get; set; }
    public int TotalSolved { get; set; }
    public GameDto? Game { get; set; }
    public UserDto? User { get; set; }
    public GameUserGameAnagramDto[]? GameUserGameAnagrams { get; set; }
}