using CodePrep.Domain.Common;

namespace CodePrep.Domain.Entities;

public class TestCase : BaseEntity
{
    public Guid QuestionId { get; set; }

    public Question Question { get; set; } = null!;

    public string Input { get; set; } = string.Empty;

    public string ExpectedOutput { get; set; } = string.Empty;

    public bool IsHidden { get; set; } = false;

    public int Order { get; set; }
}