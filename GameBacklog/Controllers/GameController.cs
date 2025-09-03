using GameBacklog.Domain.Entities;
using GameBacklog.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameBacklog.API.Controllers
{
    [ApiController]
    [Route("api/Games")]
    public class GameController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateGame([FromBody] string title)
        {
            return Ok();
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
