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
        public async Task<IActionResult> GetGame(string guid)
        {
            Guid parsedGuid;

            if (string.IsNullOrEmpty(guid) || !Guid.TryParse(guid, out parsedGuid))
            {
                return BadRequest("Guid is empty or invalid.");
            }

            var game = await _gameService.GetGameAsync(parsedGuid);

            if (game == null)
            {
                return BadRequest("Failed to find a matching game.");
            }

            return Ok(game);
        }

        [HttpGet]
        [Route("Interval")]
        public IActionResult GetGames([FromBody] int amount)
        {
            // TODO: Might need another endpoint to stream game images

            return Ok();
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
