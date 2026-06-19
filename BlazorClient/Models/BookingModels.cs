namespace BlazorClient.Models;

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

    public string StatusBadgeClass => Status switch
    {
        "Confirmed" => "badge-success",
        "Cancelled" => "badge-danger",
        "Expired" => "badge-secondary",
        _ => "badge-warning"
    };

    public int Days => (EndDate - StartDate).Days;
}

public class CreateBookingDto
{
    public string BookingType { get; set; } = "Hotel";
    public Guid ReferenceId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
    public string? Notes { get; set; }
}
