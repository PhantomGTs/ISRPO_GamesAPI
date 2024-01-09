using GamesAPI.Data.Dtos;

namespace GamesAPI.Repository.Interfaces;

public interface IGameService
{
    List<GameDto> GetGames();
    GameDto? GetGame(int id);
    void AddGame(string name, string description);
    bool DeleteGame(int id);
    bool UpdateGame(int id, string name, string description);

}