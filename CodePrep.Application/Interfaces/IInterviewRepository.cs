using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;

public interface IInterviewRepository
{
    Task<IEnumerable<InterviewQuestion>> GetAllAsync();

    Task<InterviewQuestion?> GetByIdAsync(Guid id);

    Task<IEnumerable<InterviewQuestion>> GetByTopicAsync(Guid topicId);

    Task AddAsync(InterviewQuestion entity);

    Task UpdateAsync(InterviewQuestion entity);

    Task DeleteAsync(InterviewQuestion entity);
    Task<int> CountAsync();
}