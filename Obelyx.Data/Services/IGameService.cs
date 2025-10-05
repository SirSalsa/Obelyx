using Obelyx.Core.Entities;
using Obelyx.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Obelyx.Data.Services
{
    public interface IGameService
    {
        Task<Game> CreateGameAsync(string title);
        Task<Game?> GetGameAsync(Guid guid);
        Task<IEnumerable<Game>> GetGamesAsync(GamesGetRequest gamesGetRequest);
        Task<Game> UpdateGameAsync(GameUpdateRequest request);
        Task<bool> DeleteGameAsync(Guid guid);
        Task<Game> UpdateCoverAsync(Guid id, IFormFile coverImage);
    }
}
