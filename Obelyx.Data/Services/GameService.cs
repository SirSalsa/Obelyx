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
                StartDate = request.StartDate,
                FinishedDate = request.FinishedDate,
                Score = request.Score,
                HoursPlayed = request.HoursPlayed,
                RolledCredits = request.RolledCredits,
                Notes = request.Notes
            };

            _appDbContext.Games.Add(game);

            await _appDbContext.SaveChangesAsync();

            return game;
        }

        public async Task<Game?> GetGameAsync(Guid guid)
        {
            var game = await _appDbContext.Games.FirstOrDefaultAsync(g => g.Id == guid && g.IsArchived == false);

            return game;
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(GamesGetRequest request)
        {
            var query = _appDbContext.Games.AsQueryable();

            // Apply sorting (before Skip/Take)
            query = request.SortBy switch
            {
                "StartDate" => query.OrderBy(g => g.StartDate == null)
                                .ThenByDescending(g => g.StartDate),
                "FinishedDate" => query.OrderBy(g => g.FinishedDate == null)
                                .ThenByDescending(g => g.FinishedDate),
                "Score" => query.OrderBy(g => g.Score == null)
                                .ThenByDescending(g => g.Score),
                "HoursPlayed" => query.OrderBy(g => g.HoursPlayed == null)
                                .ThenByDescending(g => g.HoursPlayed),
                _ => query.OrderBy(g => g.Title),
            };

            // Apply eventual filter for backlog status
            if (request.StatusFilter != null)
            {
                query = query.Where(g => g.BacklogStatus == request.StatusFilter);
            }

            // Filter games with a non-null score of at least 1
            if (request.MinScore is >= 1)
            {
                query = query.Where(g => g.Score.HasValue && g.Score > 0 && g.Score >= request.MinScore);
            }

            // Filter entries only if RolledCreditsOnly is true
            if (request.RolledCreditsOnly == true)
            {
                query = query.Where(g => g.RolledCredits);
            }

            // Auto-exclude archived entries
            query = query.Where(g => g.IsArchived == false);

            // Apply pagination AFTER sorting
            query = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            // Execute query
            var games = await query.ToListAsync();

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
            game.BacklogStatus = request.BacklogStatus ?? game.BacklogStatus;
            game.StartDate = request.StartDate ?? game.StartDate;
            game.FinishedDate = request.FinishedDate ?? game.FinishedDate;
            game.Score = request.Score ?? game.Score;
            game.HoursPlayed = request.HoursPlayed ?? game.HoursPlayed;
            game.RolledCredits = request.RolledCredits; // always has a value
            game.Notes = request.Notes ?? game.Notes;

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

        public async Task<bool> ArchiveGameAsync(Guid guid)
        {
            var game = _appDbContext.Games.FirstOrDefault(g => g.Id == guid);

            if (game == null)
            {
                return false;
            }

            game.IsArchived = true;

            _appDbContext.Games.Update(game);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
