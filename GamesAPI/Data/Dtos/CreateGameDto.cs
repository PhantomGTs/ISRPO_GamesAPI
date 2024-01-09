namespace GamesAPI.Data.Dtos;

public class CreateGameDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public CreateGameDto()
    {
    }
    public CreateGameDto(string name, string description)
    {
        Name = name;
        Description = description;
    }
}