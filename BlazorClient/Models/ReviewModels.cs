namespace BlazorClient.Models;

public class ReviewDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ReviewType { get; set; } = string.Empty;
    public Guid ReferenceId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string StarsDisplay => new string('★', Rating) + new string('☆', 5 - Rating);
}

public class CreateReviewDto
{
    public string ReviewType { get; set; } = string.Empty;
    public Guid ReferenceId { get; set; }
    public int Rating { get; set; } = 5;
    public string? Comment { get; set; }
}
