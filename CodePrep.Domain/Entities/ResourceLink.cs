using CodePrep.Domain.Common;

namespace CodePrep.Domain.Entities;

public class ResourceLink : BaseEntity
{
    public Guid TopicId { get; set; }

    public Topic Topic { get; set; } = default!;

    public string Title { get; set; } = string.Empty;

    public string Website { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}