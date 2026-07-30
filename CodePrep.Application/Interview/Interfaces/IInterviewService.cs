using CodePrep.Application.Interview.DTOs;

namespace CodePrep.Application.Interview.Interfaces;

public interface IInterviewService
{
    Task<IEnumerable<InterviewQuestionDto>> GetAllAsync();

    Task<InterviewQuestionDto?> GetByIdAsync(Guid id);

    Task<IEnumerable<InterviewQuestionDto>> GetByTopicAsync(Guid topicId);

    Task<Guid> CreateAsync(CreateInterviewQuestionDto dto);

    Task<bool> UpdateAsync(UpdateInterviewQuestionDto dto);

    Task<bool> DeleteAsync(Guid id);
}