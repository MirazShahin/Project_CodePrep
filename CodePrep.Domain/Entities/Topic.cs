using CodePrep.Domain.Common;

namespace CodePrep.Domain.Entities;

public class Topic : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string CategoryName { get; set; } = "";

    public bool IsActive { get; set; } = true;

    public ICollection<QuestionTopic> QuestionTopics { get; set; }
        = new List<QuestionTopic>();

    public ICollection<LearningContent> LearningContents { get; set; }
        = new List<LearningContent>();

    public ICollection<Problem> Problems { get; set; }
        = new List<Problem>();

    public ICollection<ResourceLink> Resources { get; set; }
        = new List<ResourceLink>();
}
