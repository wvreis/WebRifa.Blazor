using Microsoft.AspNetCore.Mvc;

namespace WebRifa.Blazor.Services.ErrorServices;

public class ErrorHandlingService : IErrorHandlingService {
    public ProblemDetails ErrorDetails { get; set; } = new();
}
