using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;

public interface IQuestionRepository
{
    Task AddAsync(Question question);

    Task<Question?> GetByIdAsync(Guid id);

    Task<List<Question>> GetAllAsync();

    Task UpdateAsync(Question question);

    Task DeleteAsync(Question question);

    Task SaveChangesAsync();
}