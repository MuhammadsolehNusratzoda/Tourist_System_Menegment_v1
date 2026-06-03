public class CreateGuideDto
{
    public Guid UserId { get; set; }
    public string? Bio { get; set; }
    public string Languages { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public int ExperienceYears { get; set; }
    public string? ImageUrl { get; set; }
}