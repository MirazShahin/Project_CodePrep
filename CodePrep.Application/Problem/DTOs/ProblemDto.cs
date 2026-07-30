namespace CodePrep.Application.Problem.DTOs;

public class ProblemDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public Guid TopicId { get; set; }

    public string TopicName { get; set; } = string.Empty;

    public string Judge { get; set; } = string.Empty;

    public string ProblemType { get; set; } = string.Empty;

    public string Difficulty { get; set; } = string.Empty;

    public int? Rating { get; set; }

    public string ProblemUrl { get; set; } = string.Empty;

    public bool IsPublished { get; set; }

    public string? Tags { get; set; }
}