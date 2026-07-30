namespace CodePrepBlazor.Services;

public class ToastService
{
    public event Action<ToastMessage>? OnShow;

    public void Show(string message, string type = "info") =>
        OnShow?.Invoke(new ToastMessage(message, type));

    public void ShowSuccess(string message) => Show(message, "success");
    public void ShowError(string message) => Show(message, "error");
    public void ShowInfo(string message) => Show(message, "info");
    public void ShowWarning(string message) => Show(message, "warning");
}

public record ToastMessage(string Text, string Type);
