using App.Metrics;
using App.Metrics.Counter;

namespace GamesAPI;

public class MetricsRegistry
{
    public static CounterOptions CreatedGamesCounter => new CounterOptions
    {
        Name = "Created Games",
        Context = "GamesApi",
        MeasurementUnit = Unit.Calls
    };
    public static CounterOptions DeletedGamesCounter => new CounterOptions
    {
        Name = "Deleted Games",
        Context = "GamesApi",
        MeasurementUnit = Unit.Calls
    };
    
    public static CounterOptions UpdatedGamesCounter => new CounterOptions
    {
        Name = "Updated Games",
        Context = "GamesApi",
        MeasurementUnit = Unit.Calls
    };
    
    public static CounterOptions ReadGamesCounter => new CounterOptions
    {
        Name = "Read Games",
        Context = "GamesApi",
        MeasurementUnit = Unit.Calls
    };
}