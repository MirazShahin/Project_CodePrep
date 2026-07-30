using CodePrep.Domain.Common;

namespace CodePrep.Domain.Entities;

public class InterviewQuestion : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Question { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Difficulty { get; set; } = "Easy";

    public Guid TopicId { get; set; }

    public Topic Topic { get; set; } = null!;

    public bool IsPublished { get; set; } = true;
}