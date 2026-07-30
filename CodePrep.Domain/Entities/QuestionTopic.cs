using CodePrep.Domain.Common;

namespace CodePrep.Domain.Entities;

public class QuestionTopic : BaseEntity
{
    public Guid QuestionId { get; set; }

    public Question Question { get; set; } = null!;

    public Guid TopicId { get; set; }

    public Topic Topic { get; set; } = null!;
}