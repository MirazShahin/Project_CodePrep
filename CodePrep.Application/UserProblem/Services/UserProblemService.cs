using CodePrep.Application.Interfaces;
using CodePrep.Application.UserProblem.DTOs;
using CodePrep.Application.UserProblem.Interfaces;
using UserProblemEntity = CodePrep.Domain.Entities.UserProblem;

namespace CodePrep.Application.UserProblem.Services;

public class UserProblemService : IUserProblemService
{
    private readonly IUserProblemRepository _repository;

    public UserProblemService(IUserProblemRepository repository)
    {
        _repository = repository;
    }

    public async Task MarkSolvedAsync(Guid userId, Guid problemId)
    {
        var progress = await _repository.GetAsync(userId, problemId);

        if (progress == null)
        {
            progress = new UserProblemEntity
            {
                UserId = userId,
                ProblemId = problemId,
                IsSolved = true,
                SolvedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(progress);
        }
        else
        {
            progress.IsSolved = true;
            progress.SolvedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(progress);
        }

        await _repository.SaveChangesAsync();
    }

    public async Task RemoveSolvedAsync(Guid userId, Guid problemId)
    {
        var progress = await _repository.GetAsync(userId, problemId);

        if (progress == null)
            return;

        progress.IsSolved = false;
        progress.SolvedAt = null;

        await _repository.UpdateAsync(progress);
        await _repository.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserProblemDto>> GetMyProblemsAsync(Guid userId)
    {
        var data = await _repository.GetByUserAsync(userId);

        return data.Select(x => new UserProblemDto
        {
            ProblemId = x.ProblemId,
            IsSolved = x.IsSolved,
            IsBookmarked = x.IsBookmarked,
            SolvedAt = x.SolvedAt
        });
    }

    public async Task<int> CountSolvedAsync(Guid userId)
    {
        return await _repository.CountSolvedAsync(userId);
    }
}