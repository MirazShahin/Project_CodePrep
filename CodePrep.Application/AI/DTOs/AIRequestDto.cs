namespace CodePrep.Application.AI.DTOs;

public class AIRequestDto
{
    public string Prompt { get; set; } = string.Empty;
    public string Language { get; set; } = "cpp";
}