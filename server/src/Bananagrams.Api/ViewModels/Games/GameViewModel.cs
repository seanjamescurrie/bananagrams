using System.Diagnostics.CodeAnalysis;
using Bananagrams.Api.ViewModels.GameAnagramTypes;
using Bananagrams.Api.ViewModels.Users;
using Bananagrams.Dal.Models;

namespace Bananagrams.Api.ViewModels.Games;

public class GameViewModel
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string? Title { get; set; }
    public int TotalAnagrams { get; set; }
    public bool Completed { get; set; }
    
    public GameAnagramTypeViewModel? GameAnagramType { get; set; }
    public UserViewModel[]? GameUsers { get; set; }
}