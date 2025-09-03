using GameBacklog.Domain.Enums;

namespace GameBacklog.Domain.Entities
{
    public class Game
    {
        // Required Info
        public Guid Id { get; set; }
        public string Title { get; set; }

        // Optional Info
        public string? ImagePath { get; set; }
        public int? ReleaseYear { get; set; }

        // Backlog Related
        public BacklogStatus BacklogStatus { get; set; } = BacklogStatus.None;
        public int? Score { get; set; }
        public int? HoursPlayed { get; set; }
        public bool RolledCredits { get; set; } = false;


        // TODO:
        // Optional Info - Platform, Genre(s)
    }
}
