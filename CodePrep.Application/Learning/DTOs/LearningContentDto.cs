namespace CodePrep.Application.Learning.DTOs;

public class LearningContentDto
{
    public Guid Id { get; set; }
    public Guid TopicId { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string TopicName { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public int EstimatedMinutes { get; set; }
}