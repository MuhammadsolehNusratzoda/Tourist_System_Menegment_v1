using System.ComponentModel.DataAnnotations;

public class CreateHotelDto
{
    [Required] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required] public string Location { get; set; } = string.Empty;
    [Range(1, 100000)] public decimal PricePerNight { get; set; }
    [Range(1, 5)] public int Stars { get; set; }
    public string? ImageUrl { get; set; }
}