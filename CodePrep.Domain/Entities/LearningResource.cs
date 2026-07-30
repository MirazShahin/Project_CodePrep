namespace CodePrep.Domain.Entities;

public class LearningResource
{
    public Guid Id { get; set; }

    public Guid TopicId { get; set; }

    public LearningTopic Topic { get; set; } = default!;

    public string Title { get; set; } = string.Empty;

    public string Website { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string Type { get; set; } = "Article";

    public int DisplayOrder { get; set; }
}