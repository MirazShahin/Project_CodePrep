namespace CodePrep.Application.AI.DTOs;

public class CPAssistantRequestDto
{
    public string ProblemStatement { get; set; } = string.Empty;

    public string UserCode { get; set; } = string.Empty;

    public string Language { get; set; } = "cpp";

    public string Input { get; set; } = string.Empty;

    public string ExpectedOutput { get; set; } = string.Empty;
}