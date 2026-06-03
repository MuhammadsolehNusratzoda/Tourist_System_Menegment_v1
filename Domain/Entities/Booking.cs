public class Booking : Base
{
    public Guid UserId { get; set; }
    public string BookingType { get; set; } = string.Empty; // Hotel, Transport, Guide
    public Guid ReferenceId { get; set; }   // ID of Hotel/Transport/Guide
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled, Expired
    public string? Notes { get; set; }
    // Navigation
    public User? User { get; set; }
}