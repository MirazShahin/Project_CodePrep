using CodePrep.Application.DTOs.Codeforces;

namespace CodePrep.Application.Interfaces
{
    public interface ICodeforcesService
    {
        Task<List<ProblemResponseDto>> GetProblemsAsync(string? tag, int minRating = 800, int maxRating = 1500, int take = 30);
    }
}