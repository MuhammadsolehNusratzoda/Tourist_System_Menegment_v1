public class CreateRestaurantDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Location { get; set; } = string.Empty;
    public string CuisineType { get; set; } = string.Empty; // Tajik, Italian …
    public string? PriceRange { get; set; }
    public string? OpeningHours { get; set; }
    public string? ImageUrl { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}