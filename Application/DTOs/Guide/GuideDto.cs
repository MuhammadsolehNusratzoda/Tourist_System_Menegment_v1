public class GuideDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string? Bio { get; set; }
    public string Languages { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public int ExperienceYears { get; set; }
    public decimal Rating { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }

}