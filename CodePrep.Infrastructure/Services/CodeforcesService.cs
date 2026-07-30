using System.Text.Json;
using CodePrep.Application.DTOs.Codeforces;
using CodePrep.Application.Interfaces;

namespace CodePrep.Infrastructure.Services
{
    public class CodeforcesService : ICodeforcesService
    {
        private readonly HttpClient _httpClient;

        public CodeforcesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProblemResponseDto>> GetProblemsAsync(string? tag, int minRating = 800, int maxRating = 1500, int take = 30)
        {
            string url = "https://codeforces.com/api/problemset.problems";
            if (!string.IsNullOrEmpty(tag))
            {
                url += $"?tags={tag}";
            }

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return new List<ProblemResponseDto>();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<CodeforcesResponseDto>(json, options);

            if (result?.Status != "OK") return new List<ProblemResponseDto>();

            var filteredProblems = result.Result.Problems
                .Where(p => p.Rating.HasValue && p.Rating >= minRating && p.Rating <= maxRating)
                .Take(take)
                .Select(p => new ProblemResponseDto
                {
                    ProblemId = $"{p.ContestId}{p.Index}",
                    Name = p.Name,
                    Rating = p.Rating,
                    Tags = p.Tags,
                    ProblemUrl = $"https://codeforces.com/problemset/problem/{p.ContestId}/{p.Index}"
                })
                .ToList();

            return filteredProblems;
        }
    }
}