namespace CodePrep.Domain.Entities;

public class LearningTopic
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public LearningCategory Category { get; set; } = default!;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Difficulty { get; set; } = "Beginner";

    public int DisplayOrder { get; set; }

    public bool IsCompleted { get; set; }

    public ICollection<LearningResource> Resources { get; set; }
        = new List<LearningResource>();
}