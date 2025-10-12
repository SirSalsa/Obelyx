using Obelyx.Core.Enums;

namespace Obelyx.Core.Models
{
    /// <summary>
    /// Update an existing game in the database.
    /// </summary>
    public class GameUpdateRequest
    {
        public required string Id { get; set; }

        public string? Title { get; set; }
        public BacklogStatus? BacklogStatus { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public int? Score { get; set; }
        public int? HoursPlayed { get; set; }
        public bool RolledCredits { get; set; } = false;
        public string? Notes { get; set; }
    }
}
