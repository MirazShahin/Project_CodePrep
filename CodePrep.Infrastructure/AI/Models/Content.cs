using System.Text.Json.Serialization;

namespace CodePrep.Infrastructure.AI.Models;

public class Content
{
    [JsonPropertyName("parts")]
    public List<Part> Parts { get; set; } = new();

    [JsonPropertyName("role")]
    public string Role { get; set; } = "user";
}