using Microsoft.AspNetCore.Mvc;

namespace WebRifa.Blazor.Services.ErrorServices;

public interface IErrorHandlingService {
    ProblemDetails ErrorDetails { get; set; }
}
