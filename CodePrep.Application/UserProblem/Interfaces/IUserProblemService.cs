using CodePrep.Application.UserProblem.DTOs;

namespace CodePrep.Application.UserProblem.Interfaces;

public interface IUserProblemService
{
    Task MarkSolvedAsync(Guid userId, Guid problemId);

    Task RemoveSolvedAsync(Guid userId, Guid problemId);

    Task<IEnumerable<UserProblemDto>> GetMyProblemsAsync(Guid userId);

    Task<int> CountSolvedAsync(Guid userId);
}