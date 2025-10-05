using Obelyx.Core.Models;
using Obelyx.Data.Services;
using Microsoft.AspNetCore.Mvc;

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
        /// Add a new blank game entry with a title.
        /// </summary>
        /// <param name="title">The game's title.</param>
        /// <param name="coverImage">The image file for the game cover.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateGame([FromForm] string title, IFormFile? coverImage)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title cannot be empty.");
            }

            var createdGame = await _gameService.CreateGameAsync(title);

            if (coverImage != null)
            {
                createdGame = await _gameService.UpdateCoverAsync(createdGame.Id, coverImage);
            }

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

            var game = await _gameService.UpdateGameAsync(updateModel);

            return Ok(game);
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

        // TODO: Endpoint to remove orphan images / imagepaths
    }
}
