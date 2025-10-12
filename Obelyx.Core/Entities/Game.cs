using Obelyx.Core.Enums;

namespace Obelyx.Core.Entities
{
    public sealed class Game
    {
        // Required Info
        public required Guid Id { get; set; }
        public required string Title { get; set; }

        // Metadata
        public string? ImagePath { get; set; }

        // Backlog Info
        public BacklogStatus BacklogStatus { get; set; } = BacklogStatus.None;
        public DateTime? StartDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public int? Score { get; set; }
        public int? HoursPlayed { get; set; }
        public bool RolledCredits { get; set; } = false;
        public string? Notes { get; set; }
        public bool IsArchived { get; set; } = false;

        // TODO:
        // Optional Info - Platform, Genre(s)
    }
}
