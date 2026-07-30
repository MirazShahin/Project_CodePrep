namespace CodePrep.Application.DTOs.TestCase;

public class CreateTestCaseRequestDto
{
    public Guid QuestionId { get; set; }

    public string Input { get; set; } = string.Empty;

    public string ExpectedOutput { get; set; } = string.Empty;

    public bool IsHidden { get; set; }

    public int Order { get; set; }
}