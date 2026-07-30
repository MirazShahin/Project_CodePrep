namespace CodePrep.Application.Topics.DTOs;

public class CreateResourceLinkDto
{
    public string Title { get; set; } = string.Empty;

    public string Website { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}