namespace CodePrep.Application.Interview.DTOs;

public class CreateInterviewQuestionDto
{
    public string Title { get; set; } = string.Empty;

    public string Question { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Difficulty { get; set; } = "Easy";

    public Guid TopicId { get; set; }
}