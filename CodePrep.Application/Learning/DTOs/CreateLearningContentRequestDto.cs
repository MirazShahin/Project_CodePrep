namespace CodePrep.Application.Learning.DTOs;

public class CreateLearningContentRequestDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int TopicId { get; set; }

    public int Difficulty { get; set; }

    public int EstimatedMinutes { get; set; }

    public bool IsPublished { get; set; } = true;

    public string? ThumbnailUrl { get; set; }
}