using CodePrep.Domain.Common;

namespace CodePrep.Domain.Entities;

public class Question : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string InputFormat { get; set; } = string.Empty;

    public string OutputFormat { get; set; } = string.Empty;

    public string Constraints { get; set; } = string.Empty;

    public string SampleInput { get; set; } = string.Empty;

    public string SampleOutput { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public string Difficulty { get; set; } = "Easy";

    public bool IsPublished { get; set; } = false;
    public ICollection<QuestionTopic> QuestionTopics { get; set; }
    = new List<QuestionTopic>();
    public ICollection<TestCase> TestCases { get; set; }
    = new List<TestCase>();
    public ICollection<Submission> Submissions { get; set; }
    = new List<Submission>();
}