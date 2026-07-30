namespace CodePrep.Application.DTOs.Codeforces
{
    public class CodeforcesResponseDto
    {
        public string Status { get; set; } = string.Empty;
        public CodeforcesResultDto Result { get; set; } = new();
    }

    public class CodeforcesResultDto
    {
        public List<CfProblemDto> Problems { get; set; } = new();
        public List<CfProblemStatisticsDto> ProblemStatistics { get; set; } = new();
    }

    public class CfProblemDto
    {
        public int ContestId { get; set; }
        public string Index { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public List<string> Tags { get; set; } = new();
    }

    public class CfProblemStatisticsDto
    {
        public int ContestId { get; set; }
        public string Index { get; set; } = string.Empty;
        public int SolvedCount { get; set; }
    }

    // Standardized DTO to send to Frontend
    public class ProblemResponseDto
    {
        public string ProblemId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public List<string> Tags { get; set; } = new();
        public string ProblemUrl { get; set; } = string.Empty;
    }
}