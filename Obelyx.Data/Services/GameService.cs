using Obelyx.Core.Entities;
using Obelyx.Core.Enums;
using Obelyx.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Obelyx.Data.Services
{
    public class GameService : IGameService
    {
        private readonly ObelyxContext _appDbContext;

        public GameService(ObelyxContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Game> CreateGameAsync(GameAddRequest request)
        {
            // Parsing status from string value
            BacklogStatus status;
            Enum.TryParse(request.Status, true, out status);

            var game = new Game
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                BacklogStatus = status,
                ReleaseYear = request.ReleaseYear,
                Score = request.Score,
                HoursPlayed = request.HoursPlayed,
                RolledCredits = request.RolledCredits
                //Notes = request.Notes - To be added
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

        public async Task<Game> UpdateGameAsync(GameUpdateRequest request)
        {
            var translatedGuid = Guid.Parse(request.Id);

            var game = await _appDbContext.Games.FirstOrDefaultAsync(g => g.Id == translatedGuid);

            if (game == null)
            {
                throw new KeyNotFoundException($"Game with id {translatedGuid} not found.");
            }

            // Update game entry based on what is not null
            game.Title = string.IsNullOrEmpty(request.Title) ? game.Title : request.Title;
            game.ReleaseYear = request.ReleaseYear ?? game.ReleaseYear;
            game.Score = request.Score ?? game.Score;
            game.HoursPlayed = request.HoursPlayed ?? game.HoursPlayed;
            game.BacklogStatus = string.IsNullOrEmpty(request.BacklogStatus)
                ? game.BacklogStatus
                : Enum.Parse<BacklogStatus>(request.BacklogStatus);

            game.RolledCredits = request.RolledCredits; // always has a value

            _appDbContext.Games.Update(game);
            await _appDbContext.SaveChangesAsync();

            return game;
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
