namespace CodePrep.Application.DTOs.Execution;

public class ExecuteCodeResponseDto
{
    public bool Success { get; set; }

    public string Output { get; set; } = string.Empty;

    public string Error { get; set; } = string.Empty;

    public double ExecutionTime { get; set; }

    public long Memory { get; set; }
}