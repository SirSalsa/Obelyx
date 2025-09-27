using GameBacklog.Core.Entities;
using GameBacklog.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<IEnumerable<Game>> GetGamesAsync(GamesGetRequest request)
        {
            // TODO: Add possible alternate ways to order based on object in request model

            // Pick out a specified part of the collection
            var games = await _appDbContext.Games
                .OrderBy(g => g.Title)
                .Skip(request.Page - 1)
                .Take(request.PageSize)
                .ToListAsync();

            return games;
        }

        public Task<Game> UpdateGameEntryAsync(GameUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteGameAsync(Guid guid)
        {
            var game = _appDbContext.Games.FirstOrDefault(g => g.Id == guid);

            if (game == null)
            {
                return false;
            }

            _appDbContext.Games.Remove(game);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Game> UpdateCoverAsync(Guid guid, IFormFile coverImage)
        {
            var game = await _appDbContext.Games.FirstOrDefaultAsync(g => g.Id == guid);

            if (game == null)
            {
                throw new KeyNotFoundException($"Game with id {guid} not found.");
            }

            if (coverImage != null && coverImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "covers");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Cover image inherits game guid
                var uniqueFileName = $"{guid}{Path.GetExtension(coverImage.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(fileStream);
                }

                game.ImagePath = $"/images/covers/{uniqueFileName}";
                _appDbContext.Games.Update(game);
                await _appDbContext.SaveChangesAsync();
            }

            return game;
        }

    }
}
