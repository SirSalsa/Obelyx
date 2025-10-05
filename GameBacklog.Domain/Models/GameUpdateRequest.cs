namespace Obelyx.Core.Models
{
    /// <summary>
    /// Update an existing game in the database.
    /// </summary>
    public class GameUpdateRequest
    {
        public required string Id { get; set; }

        public string? Title { get; set; }
        public int? ReleaseYear { get; set; }
        public string BacklogStatus { get; set; } = string.Empty;
        public int? Score { get; set; }
        public int? HoursPlayed { get; set; }
        public bool RolledCredits { get; set; } = false;
    }
}
