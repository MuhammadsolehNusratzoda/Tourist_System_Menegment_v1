public class Review : Base
{
    public Guid UserId { get; set; }
    public string ReviewType { get; set; } = string.Empty; // Place, Hotel, Guide
    public Guid ReferenceId { get; set; }
    public int Rating { get; set; } // 1–5
    public string? Comment { get; set; }
    // Navigation
    public User? User { get; set; }
}