using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;

public interface ILearningRepository
{
    Task<IEnumerable<LearningContent>> GetAllAsync();

    Task<LearningContent?> GetByIdAsync(Guid id);

    Task<IEnumerable<LearningContent>> GetByTopicAsync(Guid topicId);

    Task AddAsync(LearningContent entity);

    Task UpdateAsync(LearningContent entity);

    Task DeleteAsync(LearningContent entity);
    Task<int> CountAsync();
}