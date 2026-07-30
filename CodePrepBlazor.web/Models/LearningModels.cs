namespace CodePrepBlazor.Models;

public class LearningContentDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public Guid TopicId { get; set; }
    public string? TopicName { get; set; }
    public string? ContentType { get; set; }
    public int Order { get; set; }
}

public class InterviewQuestionDto
{
    public Guid Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string? Answer { get; set; }
    public string? Difficulty { get; set; }
    public Guid TopicId { get; set; }
    public string? TopicName { get; set; }
    public string? Category { get; set; }
}

public class DashboardDto
{
    public int TotalProblems { get; set; }
    public int SolvedProblems { get; set; }
    public int TotalTopics { get; set; }
    public int CompletedTopics { get; set; }
    public int TotalSubmissions { get; set; }
    public int AcceptedSubmissions { get; set; }
    public List<RecentActivityDto> RecentActivities { get; set; } = new();
    public Dictionary<string, int> DifficultyStats { get; set; } = new();
    public List<string> StrongTopics { get; set; } = new();
    public List<string> WeakTopics { get; set; } = new();
}

public class RecentActivityDto
{
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}

public class TutorRequest
{
    public string TargetTopic { get; set; } = string.Empty;
    public string SkillLevel { get; set; } = "Beginner";
    public int DurationInDays { get; set; } = 30;
    public int TotalQuestions { get; set; } = 10;
}

public class AIRequest
{
    public string Prompt { get; set; } = string.Empty;
    public string? Context { get; set; }
}

public class AIResponse
{
    public string Answer { get; set; } = string.Empty;
    public bool Success { get; set; } = true;
}
