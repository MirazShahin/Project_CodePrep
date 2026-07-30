using CodePrep.Domain.Common;

namespace CodePrep.Domain.Entities;

public class Problem : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public Guid TopicId { get; set; }

    public Topic Topic { get; set; } = null!;

    public string Judge { get; set; } = string.Empty;

    public string ProblemType { get; set; } = "CompetitiveProgramming";

    public string Difficulty { get; set; } = string.Empty;

    public int? Rating { get; set; }

    public string ProblemUrl { get; set; } = string.Empty;

    public bool IsPublished { get; set; } = true;

    public string? Tags { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
    public ICollection<UserProblem> UserProblems { get; set; }
    = new List<UserProblem>();
}