namespace CodePrep.Application.AI.DTOs;

public class TutorRequestDto
{
    public string TargetTopic { get; set; } = string.Empty;

    public string SkillLevel { get; set; } = "Beginner";

    public int DurationInDays { get; set; } = 30;

    public int TotalQuestions { get; set; } = 10;
}