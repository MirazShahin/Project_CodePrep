using CodePrep.Domain.Common;
using CodePrep.Domain.Enums;

namespace CodePrep.Domain.Entities;

public class Submission : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public ProgrammingLanguage Language { get; set; }
    public string SourceCode { get; set; } = string.Empty;
    public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;
    public double? ExecutionTime { get; set; }
    public long? MemoryUsed { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public string? ActualOutput { get; set; }
    public string? ErrorMessage { get; set; }
    public int PassedTestCases { get; set; }
}