public class RestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Location { get; set; } = string.Empty;
    public string CuisineType { get; set; } = string.Empty;
    public string? PriceRange { get; set; }
    public string? OpeningHours { get; set; }
    public string? ImageUrl { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}