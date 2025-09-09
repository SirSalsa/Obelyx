using GameBacklog.Core.Entities;
using GameBacklog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBacklog.Data.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _appDbContext;

        public GameService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Game> CreateGameAsync(string title)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Title = title
            };

            _appDbContext.Games.Add(game);

            await _appDbContext.SaveChangesAsync();

            return game;
        }

        public async Task<Game?> GetGameAsync(Guid guid)
        {
            var game = await _appDbContext.Games.FirstOrDefaultAsync(x => x.Id == guid);

            return game;
        }

        public Task<IEnumerable<Game>> GetGamesAsync(int amount)
        {
            throw new NotImplementedException();
        }

        public Task<Game> UpdateGameEntryAsync(GameUpdateRequest request)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteGameAsync(string guid)
        {
            throw new NotImplementedException();
        }
    }
}
