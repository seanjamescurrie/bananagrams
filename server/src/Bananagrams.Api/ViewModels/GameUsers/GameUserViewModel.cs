using System.Diagnostics.CodeAnalysis;
using Bananagrams.Api.Models.Games;
using Bananagrams.Api.ViewModels.GameUserGameAnagrams;
using Bananagrams.Api.ViewModels.Users;

namespace Bananagrams.Api.ViewModels.GameUsers;

public class GameUserViewModel
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    
    [AllowNull]
    public GameViewModel Game { get; set; }

}