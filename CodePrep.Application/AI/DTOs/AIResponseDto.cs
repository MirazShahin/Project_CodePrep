namespace CodePrep.Application.AI.DTOs;

public class AIResponseDto
{
    public bool Success { get; set; }

    public string Response { get; set; } = string.Empty;

    public string Error { get; set; } = string.Empty;
}