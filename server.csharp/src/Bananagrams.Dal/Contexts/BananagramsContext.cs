using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Bananagrams.Dal.Contexts;

public class BananagramsDatabase : BaseContext, IBananagramsDatabase
{
    public BananagramsDatabase(DbContextOptions option) : base(option) { }
    public BananagramsDatabase(string connectionString) : base(connectionString) { }
        
    public virtual DbSet<Game> Games { get; set; }
    public virtual DbSet<GameAnagram> GameAnagrams { get; set; }
    public virtual DbSet<GameAnagramType> GameAnagramTypes { get; set; }
    public virtual DbSet<GameUser> GameUsers { get; set; }
    public virtual DbSet<GameUserGameAnagram> GameUserGameAnagrams { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Word> Words { get; set; }
    public virtual DbSet<DailyWord> DailyWords { get; set; }
}