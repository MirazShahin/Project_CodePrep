using CodePrep.Application.Problem.DTOs;

namespace CodePrep.Application.Problem.Interfaces;

public interface IProblemService
{
    Task<IEnumerable<ProblemDto>> GetAllAsync();

    Task<ProblemDto?> GetByIdAsync(Guid id);

    Task<IEnumerable<ProblemDto>> GetByTopicAsync(Guid topicId);

    Task<Guid> CreateAsync(CreateProblemDto dto);

    Task<bool> UpdateAsync(UpdateProblemDto dto);

    Task<bool> DeleteAsync(Guid id);
}