using CodePrep.Application.Common.Exceptions;
using CodePrep.Application.DTOs.Submission;
using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Domain.Enums;

namespace CodePrep.Application.Services;

public class SubmissionService : ISubmissionService
{
    private readonly ISubmissionRepository _submissionRepository;
    private readonly IQuestionRepository _questionRepository;

    public SubmissionService(
        ISubmissionRepository submissionRepository,
        IQuestionRepository questionRepository)
    {
        _submissionRepository = submissionRepository;
        _questionRepository = questionRepository;
    }

    public async Task<SubmissionResponseDto> CreateAsync(
        Guid userId,
        CreateSubmissionRequestDto request)
    {
        var question = await _questionRepository.GetByIdAsync(request.QuestionId);

        if (question == null)
            throw new NotFoundException("Question not found.");

        var submission = new Submission
        {
            UserId = userId,
            QuestionId = request.QuestionId,
            Language = request.Language,
            SourceCode = request.SourceCode,
            Status = SubmissionStatus.Pending,
            SubmittedAt = DateTime.UtcNow
        };

        await _submissionRepository.AddAsync(submission);
        await _submissionRepository.SaveChangesAsync();

        return new SubmissionResponseDto
        {
            Id = submission.Id,
            UserId = submission.UserId,
            QuestionId = submission.QuestionId,
            Language = submission.Language,
            Status = submission.Status,
            ExecutionTime = submission.ExecutionTime,
            MemoryUsed = submission.MemoryUsed,
            SubmittedAt = submission.SubmittedAt
        };
    }

    public async Task<List<SubmissionResponseDto>> GetMySubmissionsAsync(Guid userId)
    {
        var submissions = await _submissionRepository.GetByUserIdAsync(userId);

        return submissions.Select(s => new SubmissionResponseDto
        {
            Id = s.Id,
            UserId = s.UserId,
            QuestionId = s.QuestionId,
            Language = s.Language,
            Status = s.Status,
            ExecutionTime = s.ExecutionTime,
            MemoryUsed = s.MemoryUsed,
            SubmittedAt = s.SubmittedAt
        }).ToList();
    }

    public async Task<List<SubmissionResponseDto>> GetQuestionSubmissionsAsync(Guid questionId)
    {
        var submissions = await _submissionRepository.GetByQuestionIdAsync(questionId);

        return submissions.Select(s => new SubmissionResponseDto
        {
            Id = s.Id,
            UserId = s.UserId,
            QuestionId = s.QuestionId,
            Language = s.Language,
            Status = s.Status,
            ExecutionTime = s.ExecutionTime,
            MemoryUsed = s.MemoryUsed,
            SubmittedAt = s.SubmittedAt
        }).ToList();
    }
}