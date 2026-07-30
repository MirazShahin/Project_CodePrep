using System.Text;
using System.Text.Json;
using CodePrep.Application.AI.DTOs;
using CodePrep.Application.AI.Interfaces;
using Microsoft.Extensions.Options;

namespace CodePrep.Infrastructure.AI;

public class GeminiProvider : IAIProvider
{
    private readonly GeminiOptions _options;
    private readonly HttpClient _httpClient;

    public GeminiProvider(HttpClient httpClient, IOptions<GeminiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    // Phase 1: সাধারণ প্রম্পটের জন্য
    public async Task<AIResponseDto> GenerateAsync(AIRequestDto request)
    {
        return await SendGroqRequestAsync(null, request.Prompt);
    }

    // Phase 2 থেকে 5: সিস্টেম ইনস্ট্রাকশনসহ সব মডিউলের জন্য
    public async Task<AIResponseDto> GenerateWithSystemInstructionAsync(string systemInstruction, string userPrompt)
    {
        return await SendGroqRequestAsync(systemInstruction, userPrompt);
    }

    // 🚀 Groq API-তে রিকোয়েস্ট পাঠানোর মেইন মেথড
    private async Task<AIResponseDto> SendGroqRequestAsync(string? systemInstruction, string userPrompt)
    {
        int maxRetries = 3;
        int delayMilliseconds = 2000;

        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                // Groq-এর OpenAI-compatible এন্ডপয়েন্ট
                var url = "https://api.groq.com/openai/v1/chat/completions";

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _options.ApiKey);

                // মেসেজ লিস্ট তৈরি (সিস্টেম ইনস্ট্রাকশন থাকলে তা প্রথমে যুক্ত হবে)
                var messages = new List<object>();

                if (!string.IsNullOrEmpty(systemInstruction))
                {
                    messages.Add(new { role = "system", content = systemInstruction });
                }

                messages.Add(new { role = "user", content = userPrompt });

                var groqRequest = new
                {
                    model = _options.Model,
                    messages = messages,
                    temperature = 0.2 // কোডিং এবং এজ-কেস অ্যানালাইসিসের জন্য কম টেম্পারেচার বেস্ট
                };

                var json = JsonSerializer.Serialize(groqRequest);
                var response = await _httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    string errorMsg = error.ToLower();

                    // রেট লিমিট বা সার্ভার ওভারলোড এরর আসলে রিট্রাই মেকানিজম
                    if ((response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable ||
                         errorMsg.Contains("429") ||
                         errorMsg.Contains("rate limit") ||
                         errorMsg.Contains("unavailable")) && i < maxRetries - 1)
                    {
                        await Task.Delay(delayMilliseconds);
                        delayMilliseconds *= 2; // এক্সপোনেনশিয়াল ব্যাক-অফ
                        continue;
                    }
                    return new AIResponseDto { Success = false, Error = $"Groq Error: {error}" };
                }

                var responseJson = await response.Content.ReadAsStringAsync();

                // Groq/OpenAI রেসপন্স JSON থেকে টেক্সট এক্সট্র্যাক্ট করা
                using var doc = JsonDocument.Parse(responseJson);
                var root = doc.RootElement;
                string answer = "";

                if (root.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                {
                    var firstChoice = choices[0];
                    if (firstChoice.TryGetProperty("message", out var message))
                    {
                        answer = message.GetProperty("content").GetString() ?? "";
                    }
                }

                return new AIResponseDto { Success = true, Response = answer };
            }
            catch (Exception ex)
            {
                if (i < maxRetries - 1)
                {
                    await Task.Delay(delayMilliseconds);
                    delayMilliseconds *= 2;
                    continue;
                }
                return new AIResponseDto { Success = false, Error = ex.Message };
            }
        }
        return new AIResponseDto { Success = false, Error = "Groq Service Unavailable after retries" };
    }
}