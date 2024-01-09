using GamesAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Data;

public class GamesDbContext : DbContext
{
    public GamesDbContext()
    {
  
    }
    public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
    {

    }

    public DbSet<Game> Games { get; set; } = null!;
  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;User ID=postgres;Password=postgres;Database=games;");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}