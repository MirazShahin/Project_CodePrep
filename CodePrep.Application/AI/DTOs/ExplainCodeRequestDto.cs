namespace CodePrep.Application.AI.DTOs;

public class ExplainCodeRequestDto
{
    public string Code { get; set; } = string.Empty;

    public string Language { get; set; } = "cpp";
}