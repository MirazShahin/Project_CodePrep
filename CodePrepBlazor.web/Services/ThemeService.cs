using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;

namespace CodePrepBlazor.Services;

public class ThemeService
{
    private readonly ProtectedLocalStorage _storage;
    private readonly IJSRuntime _js;
    private const string Key = "codeprep_theme";

    public string CurrentTheme { get; private set; } = "light";
    public event Action? OnThemeChanged;

    public ThemeService(ProtectedLocalStorage storage, IJSRuntime js)
    {
        _storage = storage;
        _js = js;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var result = await _storage.GetAsync<string>(Key);
            if (result.Success && !string.IsNullOrEmpty(result.Value))
                CurrentTheme = result.Value;
        }
        catch { }

        await ApplyThemeAsync();
    }

    public async Task ToggleAsync()
    {
        CurrentTheme = CurrentTheme == "light" ? "dark" : "light";
        try { await _storage.SetAsync(Key, CurrentTheme); } catch { }
        await ApplyThemeAsync();
        OnThemeChanged?.Invoke();
    }

    public async Task SetThemeAsync(string theme)
    {
        CurrentTheme = theme;
        try { await _storage.SetAsync(Key, CurrentTheme); } catch { }
        await ApplyThemeAsync();
        OnThemeChanged?.Invoke();
    }

    private async Task ApplyThemeAsync()
    {
        try
        {
            await _js.InvokeVoidAsync("setTheme", CurrentTheme);
        }
        catch { }
    }
}
