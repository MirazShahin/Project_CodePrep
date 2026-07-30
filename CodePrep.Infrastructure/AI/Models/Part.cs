using System.Text.Json.Serialization;

namespace CodePrep.Infrastructure.AI.Models;

public class Part
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}