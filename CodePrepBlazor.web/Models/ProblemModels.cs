namespace CodePrepBlazor.Models;

public class ProblemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Difficulty { get; set; } = "Easy";
    public Guid TopicId { get; set; }
    public string? TopicName { get; set; }
    public string? Constraints { get; set; }
    public string? SampleInput { get; set; }
    public string? SampleOutput { get; set; }
    public bool IsSolved { get; set; }
    public string Judge { get; set; } = string.Empty;
    public string ProblemType { get; set; } = string.Empty;
    public int? Rating { get; set; }
    public string ProblemUrl { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public string? Tags { get; set; }
}

public class QuestionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Difficulty { get; set; } = "Easy";
    public string? Constraints { get; set; }
    public List<TestCaseDto> TestCases { get; set; } = new();
}

public class TestCaseDto
{
    public Guid Id { get; set; }
    public string Input { get; set; } = string.Empty;
    public string ExpectedOutput { get; set; } = string.Empty;
    public bool IsSample { get; set; }
    public int Order { get; set; }
}

public class SubmissionRequest
{
    public Guid QuestionId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Language { get; set; } = "cpp";
}

public class SubmissionResult
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Output { get; set; }
    public string? Error { get; set; }
    public double? Runtime { get; set; }
    public double? Memory { get; set; }
    public DateTime SubmittedAt { get; set; }
}

public class CodeforcesProblem
{
    public string ProblemId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? Rating { get; set; }
    public List<string> Tags { get; set; } = new();
    public string ProblemUrl { get; set; } = string.Empty;
}
