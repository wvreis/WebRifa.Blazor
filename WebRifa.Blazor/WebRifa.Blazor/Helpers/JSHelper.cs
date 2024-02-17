using Microsoft.JSInterop;

namespace WebRifa.Blazor.Helpers;

public static class JSHelper {
    public static ValueTask<bool> ShowConfirmationMessage(
        this IJSRuntime runtime, 
        string message,
        string title = "Confirmação", 
        string acceptButtonMessage = "Sim",
        string declineButtonMessage = "Não") 
    {
        return runtime.InvokeAsync<bool>(
            "showConfirmationMessage", 
            title, 
            message, 
            acceptButtonMessage, 
            declineButtonMessage);
    }
}
