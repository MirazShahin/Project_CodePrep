//using 

namespace CodePrep.Application.Topics.DTOs;
public class TopicDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<ResourceLinkDto> Resources { get; set; } = new();
}

public class ResourceLinkDto
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? Website { get; internal set; }
}

public class CreateTopicRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public List<ResourceLinkDto> Resources { get; set; } = new();
}

public class UpdateTopicRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public List<ResourceLinkDto> Resources { get; set; } = new();
}

public class GroupedTopicDto
{
    public string CategoryName { get; set; } = string.Empty;
    public List<TopicDto> Topics { get; set; } = new();
}

public class ToggleProgressRequestDto
{
    public Guid TopicId { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsBookmarked { get; set; }
}

public class TopicProgressResponseDto
{
    public Guid TopicId { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsBookmarked { get; set; }
}