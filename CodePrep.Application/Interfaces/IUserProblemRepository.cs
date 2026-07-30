using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;
using UserProblemEntity = CodePrep.Domain.Entities.UserProblem;
public interface IUserProblemRepository
{
    Task<UserProblemEntity?> GetAsync(Guid userId, Guid problemId);

    Task<IEnumerable<UserProblemEntity>> GetByUserAsync(Guid userId);

    Task AddAsync(UserProblemEntity entity);

    Task UpdateAsync(UserProblemEntity entity);

    Task SaveChangesAsync();

    Task<int> CountSolvedAsync(Guid userId);
}