namespace CodePrep.Application.Learning.DTOs;

public class LearningContentResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int TopicId { get; set; }

    public string TopicName { get; set; } = string.Empty;

    public string Difficulty { get; set; } = string.Empty;

    public int EstimatedMinutes { get; set; }

    public bool IsPublished { get; set; }

    public string? ThumbnailUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}