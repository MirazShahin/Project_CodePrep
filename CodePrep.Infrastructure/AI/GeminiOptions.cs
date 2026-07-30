namespace CodePrep.Infrastructure.AI;

public class GeminiOptions
{
    public const string SectionName = "Groq";

    public string ApiKey { get; set; } = string.Empty;

    public string Model { get; set; } = "llama-3.1-8b-instant";
}