namespace BlazorClient.Models;

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

    public string StarsDisplay => new string('★', Stars) + new string('☆', 5 - Stars);
}

public class CreateHotelDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    public int Stars { get; set; } = 3;
    public string? ImageUrl { get; set; }
}

public class UpdateHotelDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public decimal? PricePerNight { get; set; }
    public int? Stars { get; set; }
    public string? ImageUrl { get; set; }
}
