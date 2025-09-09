using GameBacklog.Core.Entities;
using GameBacklog.Core.Models;
using GameBacklog.Data.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateGame([FromBody] string title)
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

        [HttpDelete]
        public IActionResult DeleteGame([FromBody] string guid)
        {
            return Ok();
        }
    }
}
