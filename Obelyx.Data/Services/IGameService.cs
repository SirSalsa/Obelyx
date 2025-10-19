using Obelyx.Core.Entities;
using Obelyx.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Obelyx.Data.Services
{
    public interface IGameService
    {
        Task<Game> CreateGameAsync(GameAddRequest request, IFormFile? image);
        Task<Game?> GetGameAsync(Guid guid);
        Task<IEnumerable<Game>> GetGamesAsync(GamesGetRequest gamesGetRequest);
        Task<Game> UpdateGameAsync(GameUpdateRequest request, IFormFile? image);
        Task<bool> DeleteGameAsync(Guid guid);
        Task<bool> ArchiveGameAsync(Guid guid);
    }
}
