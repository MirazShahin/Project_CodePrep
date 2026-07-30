namespace CodePrep.Application.DTOs.Topics; 
public class TopicResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}