public class BookingDto
{
    public Guid Id { get; set; }
    public string BookingType { get; set; } = string.Empty;
    public Guid ReferenceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}