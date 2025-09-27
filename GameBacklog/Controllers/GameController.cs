using GameBacklog.Core.Entities;
using GameBacklog.Core.Models;
using GameBacklog.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameBacklog.API.Controllers
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
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateGame([FromQuery] string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title cannot be empty.");
            }

            var createdGame = await _gameService.CreateGameAsync(title);

            return Ok(createdGame);
        }

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

        [HttpGet]
        [Route("Interval")]
        public async Task<IActionResult> GetGames([FromQuery] GamesGetRequest getModel)
        {
            // TODO: Might need another endpoint to stream game images

            var games = await _gameService.GetGamesAsync(getModel);

            return Ok(games);
        }

        /// <summary>
        /// Update the metadata of the game entry.
        /// </summary>
        [HttpPut]
        public IActionResult UpdateLogEntry([FromBody] GameUpdateRequest updateModel)
        {
            return Ok();
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
