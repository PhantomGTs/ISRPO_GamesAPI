namespace GamesAPI.Data.Dtos;

public class GameDto
{
    public int GameId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public GameDto(int id, string name, string description)
    {
        GameId = id;
        Name = name;
        Description = description;
    }
    public GameDto(Models.Game game) 
        : this(game.GameId, game.Name, game.Description)
    {
        
    }
}