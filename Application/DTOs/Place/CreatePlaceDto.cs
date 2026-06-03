using System.ComponentModel.DataAnnotations;

public class CreatePlaceDto
{
    [Required] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required] public string Location { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? ImageUrl { get; set; }
    public decimal EntryFee { get; set; } = 0;
}