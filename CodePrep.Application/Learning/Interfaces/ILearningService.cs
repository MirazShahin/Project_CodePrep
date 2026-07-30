using CodePrep.Application.Learning.DTOs;

namespace CodePrep.Application.Learning.Interfaces;

public interface ILearningService
{
    Task<IEnumerable<LearningContentDto>> GetAllAsync();

    Task<LearningContentDto?> GetByIdAsync(Guid id);

    Task<IEnumerable<LearningContentDto>> GetByTopicAsync(Guid topicId);

    Task<Guid> CreateAsync(CreateLearningContentDto dto);

    Task<bool> UpdateAsync(UpdateLearningContentDto dto);

    Task<bool> DeleteAsync(Guid id);
}