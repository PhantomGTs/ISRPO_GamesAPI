using System.Diagnostics;
using App.Metrics;
using GamesAPI.Data;
using GamesAPI.Data.Dtos;
using GamesAPI.Data.Models;
using GamesAPI.Repository.Interfaces;

namespace GamesAPI.Repository.Services;

public class GameService : IGameService
{
    private GamesDbContext _dbContext;
    private readonly IMetrics _metrics;
    
    public GameService(GamesDbContext dbContext, IMetrics metrics)
    {
        _dbContext = dbContext;
        _metrics = metrics;
    }
    
    public List<GameDto> GetGames()
    {
        _metrics.Measure.Counter.Increment(MetricsRegistry.ReadGamesCounter);
        
        return _dbContext.Games.Select(x => new GameDto(x)).ToList();

    }

    public GameDto? GetGame(int id)
    {
        var game = _dbContext.Games.FirstOrDefault(x => x.GameId == id);
        
        _metrics.Measure.Counter.Increment(MetricsRegistry.ReadGamesCounter);

        return (game != null) ? new GameDto(game) : null;  
    }

    public void AddGame(string name, string description)
    {
        var game = new Game()
            {Name = name, Description = description};
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();
        
        _metrics.Measure.Counter.Increment(MetricsRegistry.CreatedGamesCounter);

        using (var sleepActivity = Activity.Current?.Source.StartActivity("AddNewSpan"))
        {
            Thread.Sleep(1000);
            /*using (var NewMethodAdding = Activity.Current?.Source.StartActivity("NewMethodAdding"))
            {
                Thread.Sleep(1000);
            }*/
        }
            
    }

    public bool DeleteGame(int id)
    {
        var game = _dbContext.Games.FirstOrDefault(x => x.GameId == id);
        
        if (game == null) return false;
        
        _dbContext.Games.Remove(game);
        _dbContext.SaveChanges();
        
        _metrics.Measure.Counter.Increment(MetricsRegistry.DeletedGamesCounter);
        
        return true;
    }

    public bool UpdateGame(int id, string name, string description)
    {
        var game = _dbContext.Games.FirstOrDefault(x => x.GameId == id);
        
        if (game == null) return false;

        game.Name = name;
        game.Description = description;

        
        _dbContext.SaveChanges();
        
        _metrics.Measure.Counter.Increment(MetricsRegistry.UpdatedGamesCounter);
        
        return true;
    }
}