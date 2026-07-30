using CodePrep.Domain.Common;

namespace CodePrep.Domain.Entities;

public class UserProblem : BaseEntity
{
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public Guid ProblemId { get; set; }

    public Problem Problem { get; set; } = null!;

    public bool IsSolved { get; set; }

    public bool IsBookmarked { get; set; }

    public DateTime? SolvedAt { get; set; }
}