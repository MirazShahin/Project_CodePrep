namespace CodePrep.Domain.Entities;

public class LearningCategory
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<LearningTopic> Topics { get; set; }
        = new List<LearningTopic>();
}