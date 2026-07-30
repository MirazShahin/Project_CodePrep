using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CodePrepBlazor.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CodePrepBlazor.Services;

public class AuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ProtectedLocalStorage _localStorage;
    private const string TokenKey = "codeprep_token";
    private const string UserKey = "codeprep_user";

    public event Action? OnAuthStateChanged;

    public bool IsAuthenticated { get; private set; }
    public UserInfo? CurrentUser { get; private set; }
    public string? Token { get; private set; }

    public AuthService(IHttpClientFactory httpClientFactory, ProtectedLocalStorage localStorage)
    {
        _httpClientFactory = httpClientFactory;
        _localStorage = localStorage;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var tokenResult = await _localStorage.GetAsync<string>(TokenKey);
            var userResult = await _localStorage.GetAsync<UserInfo>(UserKey);

            if (tokenResult.Success && !string.IsNullOrEmpty(tokenResult.Value))
            {
                Token = tokenResult.Value;
                CurrentUser = userResult.Success ? userResult.Value : null;
                IsAuthenticated = true;
            }
        }
        catch
        {
            // Storage not available during prerender
        }
    }

    public async Task<(bool Success, string Message)> LoginAsync(LoginRequest request)
    {
        var client = _httpClientFactory.CreateClient("CodePrepAPI");
        var response = await client.PostAsJsonAsync("api/Auth/login", request);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            return (false, ExtractMessage(err) ?? "Login failed. Check your credentials.");
        }

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        var data = json.GetProperty("data");

        var auth = new AuthResponse
        {
            Token = data.GetProperty("token").GetString() ?? "",
            UserName = data.TryGetProperty("userName", out var un) ? un.GetString() ?? "" :
                       data.TryGetProperty("username", out var un2) ? un2.GetString() ?? "" : "",
            Email = data.TryGetProperty("email", out var em) ? em.GetString() ?? "" : request.Email,
            Role = data.TryGetProperty("role", out var role) ? role.GetString() ?? "User" : "User",
            UserId = data.TryGetProperty("userId", out var uid) ? uid.GetGuid() :
                     data.TryGetProperty("id", out var id) ? id.GetGuid() : Guid.Empty
        };

        Token = auth.Token;
        CurrentUser = new UserInfo
        {
            Id = auth.UserId,
            UserName = auth.UserName,
            Email = auth.Email,
            Role = auth.Role
        };
        IsAuthenticated = true;

        await _localStorage.SetAsync(TokenKey, Token);
        await _localStorage.SetAsync(UserKey, CurrentUser);
        OnAuthStateChanged?.Invoke();

        return (true, "Login successful!");
    }

    public async Task<(bool Success, string Message)> RegisterAsync(RegisterRequest request)
    {
        var client = _httpClientFactory.CreateClient("CodePrepAPI");
        var response = await client.PostAsJsonAsync("api/Auth/register", request);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            return (false, ExtractMessage(err) ?? "Registration failed.");
        }

        return (true, "Registration successful! Please login.");
    }

    public async Task LogoutAsync()
    {
        Token = null;
        CurrentUser = null;
        IsAuthenticated = false;

        try
        {
            await _localStorage.DeleteAsync(TokenKey);
            await _localStorage.DeleteAsync(UserKey);
        }
        catch { }

        OnAuthStateChanged?.Invoke();
    }

    public HttpClient GetAuthenticatedClient()
    {
        var client = _httpClientFactory.CreateClient("CodePrepAPI");
        if (!string.IsNullOrEmpty(Token))
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Token);
        }
        return client;
    }

    private static string? ExtractMessage(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("message", out var msg))
                return msg.GetString();
            if (doc.RootElement.TryGetProperty("title", out var title))
                return title.GetString();
        }
        catch { }
        return null;
    }
}
