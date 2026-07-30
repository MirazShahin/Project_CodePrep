using System.Text.Json.Serialization;

namespace CodePrep.Infrastructure.AI.Models;

public class GeminiRequest
{
    [JsonPropertyName("contents")]
    public List<Content> Contents { get; set; } = new();
}