using GamesAPI.Data.Dtos;
using GamesAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public IEnumerable<GameDto> GetGames()
    {
        return _gameService.GetGames();
    }
    
    [HttpGet("{id}")]
    public IActionResult GetGame([FromRoute]int id)
    {
        var game = _gameService.GetGame(id);
        return (game != null) ? Ok(game) : NotFound();    
    }

    [HttpPost]
    public void AddGame([FromBody]CreateGameDto dto)
    {
        _gameService.AddGame(dto.Name, dto.Description);
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteGame([FromRoute]int id)
    {
        var result = _gameService.DeleteGame(id);
        
        return (result) ? Ok(result) : BadRequest("Данной игры не существует!");

    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateGame([FromRoute]int id, [FromBody]CreateGameDto dto)
    {
        var result = _gameService.UpdateGame(id, dto.Name, dto.Description);
        
        return (result) ? Ok(result) : BadRequest("Данной игры не существует!");

    }
}