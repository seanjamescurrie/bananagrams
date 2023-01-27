using System.Diagnostics.CodeAnalysis;
using Bananagrams.Service.Dtos.GameAnagrams;
using Bananagrams.Service.Dtos.GameUsers;

namespace Bananagrams.Service.Dtos.GameUserGameAnagrams;

public class GameUserGameAnagramDto
{
    public int Id { get; set; }
    public int Attempts { get; set; }
    public DateTime DatePlayed { get; set; }
    public DateTime DateSolved { get; set; }
    public int GameUserId { get; set; }
    public int GameAnagramId { get; set; }
    
    public GameUserDto? GameUser { get; set; }
    public GameAnagramDto? GameAnagram { get; set; }
}