﻿using Newtonsoft.Json;
using System.Net;

namespace WebRifa.Blazor.Exceptions;
public class ExceptionMiddleware {

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try {
            await _next(context);
        }
        catch (Exception ex) {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = GetStatusCodeAsync(ex);

        var response = new
        {
            error = new
            {
                message = $"{ex.GetType().Name}: An error occurred while processing your request.",
                details = ex.Message
            }
        };

        _logger.LogError(response.ToString());

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
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