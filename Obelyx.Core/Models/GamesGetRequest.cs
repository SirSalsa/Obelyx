using Obelyx.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Obelyx.Core.Models
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

        /// <summary>
        /// How the returned games are sorted.
        /// </summary>
        public string? SortBy { get; set; } = "Title";

        /// <summary>
        /// What BacklogStatus to filter by.
        /// </summary>
        public BacklogStatus? StatusFilter { get; set; }
    }
}
