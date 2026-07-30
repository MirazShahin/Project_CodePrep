using CodePrep.Application.Topics.DTOs;

namespace CodePrep.Application.DTOs.Topic;
public class TopicDetailsDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public List<ResourceLinkDto> Resources { get; set; } = new();
}