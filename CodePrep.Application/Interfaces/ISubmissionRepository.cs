using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;

public interface ISubmissionRepository
{
    Task AddAsync(Submission submission);

    Task<Submission?> GetByIdAsync(Guid id);

    Task<List<Submission>> GetByUserIdAsync(Guid userId);

    Task<List<Submission>> GetByQuestionIdAsync(Guid questionId);

    Task UpdateAsync(Submission submission);

    Task SaveChangesAsync();
}