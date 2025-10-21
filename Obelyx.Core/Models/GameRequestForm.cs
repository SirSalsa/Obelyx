using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Obelyx.Core.Models
{
    /// <summary>
    /// Wrapper for game data and cover image.
    /// </summary>
    public class GameRequestForm
    {
        [FromForm(Name = "gameData")]
        public string GameData { get; set; } = string.Empty;

        [FromForm(Name = "coverImage")]
        public IFormFile? CoverImage { get; set; }
    }
}
