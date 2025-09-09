using System.ComponentModel.DataAnnotations;

namespace GameBacklog.Core.Models
{
    public class GamesGetRequest
    {
        /// <summary>
        /// Page number (1-based).
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// How many games per page.
        /// </summary>
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        // TODO: Add optional filters like ...
    }
}
