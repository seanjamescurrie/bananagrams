using System.Diagnostics.CodeAnalysis;
using Bananagrams.Api.ViewModels.Games;
using Bananagrams.Api.ViewModels.GameUserGameAnagrams;
using Bananagrams.Api.ViewModels.Users;

namespace Bananagrams.Api.ViewModels.GameUsers;

public class GameUserDetailViewModel
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    
    [AllowNull]
    public GameViewModel Game { get; set; }
    [AllowNull]
    public UserViewModel User { get; set; }
    [AllowNull]
    public GameUserGameAnagramViewModel[] GameUserGameAnagrams { get; set; }

}