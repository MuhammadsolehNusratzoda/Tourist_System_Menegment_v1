using System.ComponentModel.DataAnnotations;

public class CreateReviewDto
{
    [Required] public string ReviewType { get; set; } = string.Empty; // Place/Hotel/Guide
    [Required] public Guid ReferenceId { get; set; }
    [Range(1, 5)] public int Rating { get; set; }
    public string? Comment { get; set; }
}