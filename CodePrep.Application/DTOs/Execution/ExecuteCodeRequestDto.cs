using CodePrep.Domain.Enums;

namespace CodePrep.Application.DTOs.Execution;

public class ExecuteCodeRequestDto
{
    public ProgrammingLanguage Language { get; set; }

    public string SourceCode { get; set; } = string.Empty;

    public string Input { get; set; } = string.Empty;
}