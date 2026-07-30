using CodePrep.Domain.Common;
using CodePrep.Domain.Common.Enums;

namespace CodePrep.Domain.Entities;

public class LearningContent : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public Guid TopicId { get; set; }

    public Topic Topic { get; set; } = null!;

    public Difficulty Difficulty { get; set; }

    public int EstimatedMinutes { get; set; }

    public bool IsPublished { get; set; } = true;

    public string? ThumbnailUrl { get; set; }
}