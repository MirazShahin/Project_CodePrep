using CodePrep.Application.DTOs.Submission;

namespace CodePrep.Application.Interfaces;

public interface ISubmissionService
{
    Task<SubmissionResponseDto> CreateAsync(
        Guid userId,
        CreateSubmissionRequestDto request);

    Task<List<SubmissionResponseDto>> GetMySubmissionsAsync(Guid userId);

    Task<List<SubmissionResponseDto>> GetQuestionSubmissionsAsync(Guid questionId);
}