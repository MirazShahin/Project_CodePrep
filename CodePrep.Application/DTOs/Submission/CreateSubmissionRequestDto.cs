using CodePrep.Domain.Enums;

namespace CodePrep.Application.DTOs.Submission;

public class CreateSubmissionRequestDto
{
    public Guid QuestionId { get; set; }

    public ProgrammingLanguage Language { get; set; }

    public string SourceCode { get; set; } = string.Empty;
}