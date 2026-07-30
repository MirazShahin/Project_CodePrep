public class Judge0ResultDto
{
    public string? stdout { get; set; }
    public string? stderr { get; set; }
    public string? compile_output { get; set; }

    public StatusDto status { get; set; } = new();
}

public class StatusDto
{
    public int id { get; set; }
    public string description { get; set; } = string.Empty;
}