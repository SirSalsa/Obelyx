namespace Obelyx.Core.Models
{
    public class GameAddRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Status {  get; set; }
        public int? ReleaseYear { get; set; }
        public int? Score { get; set; }
        public int? HoursPlayed { get; set; }
        public bool RolledCredits { get; set; } = false;
        public string? Notes { get; set; }
    }
}
