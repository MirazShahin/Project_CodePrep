using System.Text.Json.Serialization;

namespace CodePrep.Application.DTOs.Execution;

public class Judge0SubmissionResult
{
    public string? stdout { get; set; }

    public string? stderr { get; set; }

    public string? compile_output { get; set; }

    public string? message { get; set; }

    public string? time { get; set; }

    public int? memory { get; set; }

    public Judge0Status status { get; set; } = new();
}

public class Judge0Status
{
    public int id { get; set; }

    public string description { get; set; } = string.Empty;
}