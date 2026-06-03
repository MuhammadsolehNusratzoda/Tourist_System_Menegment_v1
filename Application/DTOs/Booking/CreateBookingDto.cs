using System.ComponentModel.DataAnnotations;

public class CreateBookingDto
{
    [Required] public string BookingType { get; set; } = string.Empty; // Hotel/Transport/Guide
    [Required] public Guid ReferenceId { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    public string? Notes { get; set; }
}