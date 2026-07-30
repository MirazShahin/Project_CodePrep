namespace CodePrepBlazor.Models;

public class TopicDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public List<ResourceLinkDto> Resources { get; set; } = new();
}

public class ResourceLinkDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? Type { get; set; }
    public int DisplayOrder { get; set; }
}

public class GroupedTopicsDto
{
    public List<TopicDto> CompetitiveProgramming { get; set; } = new();
    public List<TopicDto> InterviewPrep { get; set; } = new();
}

public class CreateTopicRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CategoryName { get; set; } = "Competitive Programming";
    public string? Icon { get; set; }
}
