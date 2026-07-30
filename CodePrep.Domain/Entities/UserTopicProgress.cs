namespace CodePrep.Domain.Entities;

public class UserTopicProgress
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = string.Empty;

    public Guid TopicId { get; set; }
    public Topic? Topic { get; set; }

    public bool IsCompleted { get; set; }
    public bool IsBookmarked { get; set; }
    public DateTime? CompletedAt { get; set; }
}