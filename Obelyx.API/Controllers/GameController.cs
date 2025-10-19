using Microsoft.AspNetCore.Mvc;
using Obelyx.Core.Models;
using Obelyx.Data.Services;
using System.Text.Json;

namespace Obelyx.API.Controllers
{
    [ApiController]
    [Route("api/Games")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Create a new game entry, optionally with an associated cover image.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateGame([FromForm] CreateGameForm form)
        {
            if (string.IsNullOrWhiteSpace(form.GameData))
            {
                return BadRequest("Game data is required.");
            }

            var request = JsonSerializer.Deserialize<GameAddRequest>(
                form.GameData,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (request is null || string.IsNullOrEmpty(request.Title))
            {
                return BadRequest("Title cannot be empty.");
            }

            var createdGame = await _gameService.CreateGameAsync(request, form.CoverImage);

            return Ok(createdGame);
        }

        /// <summary>
        /// Get the corresponding game entry.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{guid:guid}")]
        public async Task<IActionResult> GetGame(string guid)
        {
            if (string.IsNullOrEmpty(guid) || !Guid.TryParse(guid, out var parsedGuid))
            {
                return BadRequest("Guid is empty or invalid.");
            }

            var game = await _gameService.GetGameAsync(parsedGuid);

            if (game == null)
            {
                return NotFound($"Game with id {parsedGuid} not found.");
            }

            return Ok(game);
        }

        /// <summary>
        /// Get a batched list of game entries.
        /// </summary>
        /// <param name="getModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Interval")]
        public async Task<IActionResult> GetGames([FromQuery] GamesGetRequest getModel)
        {
            var games = await _gameService.GetGamesAsync(getModel);

            return Ok(games);
        }

        /// <summary>
        /// Update the metadata of the game entry.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateLogEntry([FromBody] GameUpdateRequest updateModel)
        {
            if (string.IsNullOrEmpty(updateModel.Id))
            {
                return BadRequest("Id is empty or invalid.");
            }

            var game = await _gameService.UpdateGameAsync(updateModel, null);

            return Ok(game);
        }

        /// <summary>
        /// Archive the specified game entry, hiding it from the client.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Archive")]
        public async Task<IActionResult> ArchiveGame([FromQuery] string guid)
        {
            if (string.IsNullOrEmpty(guid) || !Guid.TryParse(guid, out var parsedGuid))
            {
                return BadRequest("Guid is empty or invalid.");
            }

            var response = await _gameService.ArchiveGameAsync(parsedGuid);

            if (response == false)
            {
                return NotFound($"Game with id {parsedGuid} not found.");
            }

            return Ok("Archived game successfully!");
        }

        /// <summary>
        /// Delete the specified game entry.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteGame([FromQuery] string guid)
        {
            if (string.IsNullOrEmpty(guid) || !Guid.TryParse(guid, out var parsedGuid))
            {
                return BadRequest("Guid is empty or invalid.");
            }

            var response = await _gameService.DeleteGameAsync(parsedGuid);

            if (response == false)
            {
                return NotFound($"Game with id {parsedGuid} not found.");
            }

            return Ok();
        }
    }
}
