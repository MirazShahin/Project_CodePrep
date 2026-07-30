using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;
using ProblemEntity = CodePrep.Domain.Entities.Problem;
public interface IProblemRepository
{
    Task<IEnumerable<ProblemEntity>> GetAllAsync();

    Task<ProblemEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<ProblemEntity>> GetByTopicAsync(Guid topicId);

    Task AddAsync(ProblemEntity problem);

    Task UpdateAsync(ProblemEntity problem);

    Task DeleteAsync(ProblemEntity problem);

    Task SaveChangesAsync();
    Task<int> CountAsync();
}