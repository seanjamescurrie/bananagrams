using Bananagrams.Api.ViewModels.GameAnagrams;
using Bananagrams.Api.ViewModels.GameUsers;

namespace Bananagrams.Api.ViewModels.GameUserGameAnagrams;

public class GameUserGameAnagramViewModel
{
    public int Id { get; set; }
    public int Attempts { get; set; }
    public DateTime DatePlayed { get; set; }
    public DateTime DateSolved { get; set; }
    public int GameAnagramId { get; set; }
    public int GameUserId { get; set; }
}