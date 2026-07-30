namespace CodePrep.Application.Problem.DTOs;

public class CreateProblemDto
{
    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public Guid TopicId { get; set; }

    public string Judge { get; set; } = string.Empty;

    public string ProblemType { get; set; } = "CompetitiveProgramming";

    public string Difficulty { get; set; } = string.Empty;

    public int? Rating { get; set; }

    public string ProblemUrl { get; set; } = string.Empty;

    public string? Tags { get; set; }
}