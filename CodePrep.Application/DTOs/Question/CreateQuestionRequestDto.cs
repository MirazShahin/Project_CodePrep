namespace CodePrep.Application.DTOs.Question;

public class CreateQuestionRequestDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Difficulty { get; set; } = "Easy";

    public string InputFormat { get; set; } = string.Empty;

    public string OutputFormat { get; set; } = string.Empty;

    public string Constraints { get; set; } = string.Empty;

    public string SampleInput { get; set; } = string.Empty;

    public string SampleOutput { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;
}