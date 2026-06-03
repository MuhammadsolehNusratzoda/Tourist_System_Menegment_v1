public class HotelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Location { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    public int Stars { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}