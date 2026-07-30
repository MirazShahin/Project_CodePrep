namespace CodePrep.Application.Learning.DTOs;

public class UpdateLearningContentRequestDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int TopicId { get; set; }

    public int Difficulty { get; set; }

    public int EstimatedMinutes { get; set; }

    public bool IsPublished { get; set; }

    public string? ThumbnailUrl { get; set; }
}