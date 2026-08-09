using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Trivo.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = Guid.NewGuid().ToString();
        context.Response.Headers["trace-id"] = traceId;

        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            logger.LogError("Validation error caught: {Message} TraceId:{TraceId} Path:{Path}", ex.Message, traceId, context.Request.Path);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation errors",
                Detail = "You have one or more validation errors",
                Type = "ValidationFailure",
                Instance = context.Request.Path
            };

            problemDetails.Extensions["errors"] = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled general exception");

            var problemDetails = new ProblemDetails
            {
                Title = "Server error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
