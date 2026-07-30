using CodePrep.Domain.Enums;

namespace CodePrep.Application.DTOs.Submission;

public class SubmissionResponseDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid QuestionId { get; set; }

    public ProgrammingLanguage Language { get; set; }

    public SubmissionStatus Status { get; set; }

    public double? ExecutionTime { get; set; }

    public long? MemoryUsed { get; set; }

    public DateTime SubmittedAt { get; set; }
}