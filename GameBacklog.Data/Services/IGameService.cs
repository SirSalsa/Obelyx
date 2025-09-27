using GameBacklog.Core.Entities;
using GameBacklog.Core.Models;

namespace GameBacklog.Data.Services
{
    public interface IGameService
    {
        Task<Game> CreateGameAsync(string title);
        Task<Game?> GetGameAsync(Guid guid);
        Task<IEnumerable<Game>> GetGamesAsync(GamesGetRequest gamesGetRequest);
        Task<Game> UpdateGameEntryAsync(GameUpdateRequest request);
        Task<bool> DeleteGameAsync(Guid guid);
    }
}
