using System.Net.Http.Json;
using System.Text.Json;
using CodePrepBlazor.Models;

namespace CodePrepBlazor.Services;

public class ApiClient
{
    private readonly AuthService _auth;
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiClient(AuthService auth) => _auth = auth;

    private HttpClient Client => _auth.GetAuthenticatedClient();

    // ── Topics ──────────────────────────────────────────────
    public async Task<List<TopicDto>> GetTopicsAsync()
    {
        var res = await Client.GetFromJsonAsync<List<TopicDto>>("api/Topic", JsonOpts);
        return res ?? new();
    }

    public async Task<TopicDto?> GetTopicAsync(Guid id)
        => await Client.GetFromJsonAsync<TopicDto>($"api/Topic/{id}", JsonOpts);

    public async Task<List<TopicDto>> SearchTopicsAsync(string? keyword, string? category = null)
    {
        var qs = $"api/Topic/search?keyword={Uri.EscapeDataString(keyword ?? "")}";
        if (!string.IsNullOrEmpty(category))
            qs += $"&category={Uri.EscapeDataString(category)}";
        var res = await Client.GetFromJsonAsync<List<TopicDto>>(qs, JsonOpts);
        return res ?? new();
    }

    public async Task<object?> GetGroupedTopicsAsync()
        => await Client.GetFromJsonAsync<object>("api/Topic/grouped", JsonOpts);

    // ── Problems ────────────────────────────────────────────
    public async Task<List<ProblemDto>> GetProblemsAsync()
    {
        var res = await Client.GetFromJsonAsync<List<ProblemDto>>("api/Problem", JsonOpts);
        return res ?? new();
    }

    public async Task<ProblemDto?> GetProblemAsync(Guid id)
        => await Client.GetFromJsonAsync<ProblemDto>($"api/Problem/{id}", JsonOpts);

    public async Task<List<ProblemDto>> GetProblemsByTopicAsync(Guid topicId)
    {
        var res = await Client.GetFromJsonAsync<List<ProblemDto>>($"api/Problem/topic/{topicId}", JsonOpts);
        return res ?? new();
    }

    // ── Questions ───────────────────────────────────────────
    public async Task<List<QuestionDto>> GetQuestionsAsync()
    {
        var res = await Client.GetFromJsonAsync<List<QuestionDto>>("api/Question", JsonOpts);
        return res ?? new();
    }

    public async Task<QuestionDto?> GetQuestionAsync(Guid id)
        => await Client.GetFromJsonAsync<QuestionDto>($"api/Question/{id}", JsonOpts);

    // ── Learning ────────────────────────────────────────────
    public async Task<List<LearningContentDto>> GetLearningContentAsync()
    {
        var res = await Client.GetFromJsonAsync<List<LearningContentDto>>("api/Learning", JsonOpts);
        return res ?? new();
    }

    public async Task<List<LearningContentDto>> GetLearningByTopicAsync(Guid topicId)
    {
        var res = await Client.GetFromJsonAsync<List<LearningContentDto>>($"api/Learning/topic/{topicId}", JsonOpts);
        return res ?? new();
    }

    // ── Interview ───────────────────────────────────────────
    public async Task<List<InterviewQuestionDto>> GetInterviewQuestionsAsync()
    {
        var res = await Client.GetFromJsonAsync<List<InterviewQuestionDto>>("api/Interview", JsonOpts);
        return res ?? new();
    }

    public async Task<List<InterviewQuestionDto>> GetInterviewByTopicAsync(Guid topicId)
    {
        var res = await Client.GetFromJsonAsync<List<InterviewQuestionDto>>($"api/Interview/topic/{topicId}", JsonOpts);
        return res ?? new();
    }

    // ── Dashboard ───────────────────────────────────────────
    public async Task<DashboardDto?> GetDashboardAsync(Guid userId)
        => await Client.GetFromJsonAsync<DashboardDto>($"api/Dashboard/{userId}", JsonOpts);

    // ── Codeforces ──────────────────────────────────────────
    public async Task<List<CodeforcesProblem>> GetCodeforcesProblemsAsync(string? tag = null, int minRating = 800, int maxRating = 1500)
    {
        var qs = $"api/CPProblems/cf-problems?minRating={minRating}&maxRating={maxRating}";
        if (!string.IsNullOrEmpty(tag)) qs += $"&tag={Uri.EscapeDataString(tag)}";
        try
        {
            var res = await Client.GetFromJsonAsync<List<CodeforcesProblem>>(qs, JsonOpts);
            return res ?? new();
        }
        catch
        {
            // fallback path without nested route
            qs = $"api/CPProblems?minRating={minRating}&maxRating={maxRating}";
            if (!string.IsNullOrEmpty(tag)) qs += $"&tag={Uri.EscapeDataString(tag)}";
            var res = await Client.GetFromJsonAsync<List<CodeforcesProblem>>(qs, JsonOpts);
            return res ?? new();
        }
    }

    // ── Submissions ─────────────────────────────────────────
    public async Task<SubmissionResult?> SubmitCodeAsync(SubmissionRequest request)
    {
        var response = await Client.PostAsJsonAsync("api/Submission", request);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<SubmissionResult>(JsonOpts);
    }

    // ── AI ──────────────────────────────────────────────────
    public async Task<string> AskAIAsync(string prompt)
    {
        var response = await Client.PostAsJsonAsync("api/AI/ask", new AIRequest { Prompt = prompt });
        if (!response.IsSuccessStatusCode) return "Sorry, AI is unavailable right now.";
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        if (json.TryGetProperty("answer", out var ans)) return ans.GetString() ?? "";
        if (json.TryGetProperty("response", out var resp)) return resp.GetString() ?? "";
        if (json.ValueKind == JsonValueKind.String) return json.GetString() ?? "";
        return json.ToString();
    }

    public async Task<string> GenerateRoadmapAsync(TutorRequest request)
    {
        var response = await Client.PostAsJsonAsync("api/Tutor/generate-roadmap", request);
        if (!response.IsSuccessStatusCode) return "Failed to generate roadmap.";
        var text = await response.Content.ReadAsStringAsync();
        return CleanAIResponse(text);
    }

    public async Task<string> GenerateQuizAsync(TutorRequest request)
    {
        var response = await Client.PostAsJsonAsync("api/Tutor/generate-quiz", request);
        if (!response.IsSuccessStatusCode) return "Failed to generate quiz.";
        var text = await response.Content.ReadAsStringAsync();
        return CleanAIResponse(text);
    }

    public async Task<string> ExplainCodeAsync(string language, string code)
    {
        var response = await Client.PostAsJsonAsync("api/CPAssistant/explain-code",
            new { Language = language, Code = code });
        if (!response.IsSuccessStatusCode) return "Failed to explain code.";
        var text = await response.Content.ReadAsStringAsync();
        return CleanAIResponse(text);
    }

    private static string CleanAIResponse(string raw)
    {
        try
        {
            using var doc = JsonDocument.Parse(raw);
            var root = doc.RootElement;
            if (root.ValueKind == JsonValueKind.String) return root.GetString() ?? raw;
            if (root.TryGetProperty("answer", out var a)) return a.GetString() ?? raw;
            if (root.TryGetProperty("response", out var r)) return r.GetString() ?? raw;
            if (root.TryGetProperty("content", out var c)) return c.GetString() ?? raw;
            return root.ToString();
        }
        catch
        {
            return raw.Trim('"');
        }
    }
}
