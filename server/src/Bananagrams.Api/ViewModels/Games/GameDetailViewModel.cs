using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Bananagrams.Api.ViewModels.GameAnagrams;
using Bananagrams.Api.ViewModels.GameAnagramTypes;
using Bananagrams.Api.ViewModels.GameUsers;
using Bananagrams.Api.ViewModels.Users;

namespace Bananagrams.Api.ViewModels.Games;

public class GameDetailViewModel
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public int GameAnagramTypeId { get; set; }
    public string? Title { set; get; }
    public GameAnagramTypeViewModel? GameAnagramType { get; set; }
    public GameAnagramViewModel[]? GameAnagrams { get; set; }
    public UserViewModel[]? GameUsers { get; set; }
}