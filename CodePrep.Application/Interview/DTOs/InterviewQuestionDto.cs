namespace CodePrep.Application.Interview.DTOs;

public class InterviewQuestionDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Question { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Difficulty { get; set; } = string.Empty;

    public Guid TopicId { get; set; }

    public string TopicName { get; set; } = string.Empty;

    public bool IsPublished { get; set; }
}