using System.ComponentModel.DataAnnotations;

namespace GameBacklog.Domain.Models
{
    /// <summary>
    /// Update an existing game in the database.
    /// </summary>
    public class GameUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public int? Score { get; set; }
        public int? HoursPlayed { get; set; }
        public bool RolledCredits { get; set; } = false;
    }
}
