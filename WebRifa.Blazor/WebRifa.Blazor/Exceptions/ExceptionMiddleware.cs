using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace WebRifa.Blazor.Exceptions;
public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger) {
    public async Task InvokeAsync(HttpContext context)
    {
        try {
            await next(context);
        }
        catch (Exception ex) {
            bool isApiException = context.Request.Path.Value!.StartsWith("/api/");
            var problemDetails = GetProblemDetail(context, ex);

            if (isApiException) {
                await HandleExceptionAsync(context, ex);
            }
            else {
                context.Response.Redirect($"/Error?" +
                $"&title={Uri.EscapeDataString(problemDetails.Title)}" +
                $"&detail={Uri.EscapeDataString(problemDetails.Detail)}");
            }
        }
    }
    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        ProblemDetails response = GetProblemDetail(context, ex);
        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
    private ProblemDetails GetProblemDetail(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = GetStatusCodeAsync(ex);

        var response = new ProblemDetails() {
            Type = ex.GetType().Name,
            Title = "An error occurred while processing your request.",
            Detail = ex.Message,
            Status = GetStatusCodeAsync(ex)
        };

        logger.LogError(JsonConvert.SerializeObject(response, Formatting.Indented));
        context.Items["ErrorDetails"] = response;
        return response;
    }

    int GetStatusCodeAsync(Exception ex)
    {
        switch (ex) {
            case ArgumentNullException _:
                return (int)HttpStatusCode.BadRequest;
            case KeyNotFoundException _:
                return (int)HttpStatusCode.BadRequest;
            case UnauthorizedAccessException _:
                return (int)HttpStatusCode.Unauthorized;
            default:
                return (int)HttpStatusCode.InternalServerError;
        }
    }
}