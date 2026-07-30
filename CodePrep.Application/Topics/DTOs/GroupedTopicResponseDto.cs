using CodePrep.Application.DTOs.Topics;
using CodePrep.Domain.Enums;

namespace CodePrep.Application.DTOs.Topic;

public class GroupedTopicResponseDto
{
    public string CategoryName { get; set; } = string.Empty; // "Competitive Programming" / "Interview Preparation"
    public TopicCategory Category { get; set; }
    public List<TopicResponseDto> Topics { get; set; } = new();
}