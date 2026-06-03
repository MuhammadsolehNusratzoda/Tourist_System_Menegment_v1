public class Guide : Base
{
    public Guid UserId { get; set; }        // Link to User
    public string? Bio { get; set; }
    public string Languages { get; set; } = string.Empty; // "Tajik, English"
    public decimal PricePerDay { get; set; }
    public int ExperienceYears { get; set; }
    public decimal Rating { get; set; } = 0;
    public string? ImageUrl { get; set; }
    // Navigation
    public User? User { get; set; }
    public List<Booking> Bookings { get; set; } = new List<Booking>();
    public List<Review> Reviews { get; set; } = new List<Review>();
}