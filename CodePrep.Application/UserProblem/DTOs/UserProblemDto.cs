namespace CodePrep.Application.UserProblem.DTOs;

public class UserProblemDto
{
    public Guid ProblemId { get; set; }

    public bool IsSolved { get; set; }

    public bool IsBookmarked { get; set; }

    public DateTime? SolvedAt { get; set; }
}