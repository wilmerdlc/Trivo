using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

namespace Trivo.API.Filters;

/// <summary>
/// Single source of truth for "which ErrorType produces which HTTP status code" and "what the
/// ProblemDetails body looks like" for business errors coming out of <see cref="Result"/>/<see cref="ResultT{TValue}"/>.
/// Mirrors the shape ExceptionHandlingMiddleware already uses for validation/unhandled exceptions, so every
/// error response from the API looks the same on the wire.
/// </summary>
internal static class ProblemDetailsMapper
{
    /// <summary>
    /// Default HTTP status code for an <see cref="ErrorType"/> when the <see cref="Error"/> does not carry
    /// an explicit <see cref="Error.StatusCode"/>.
    /// </summary>
    public static int ToDefaultStatusCode(this ErrorType type) => type switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorType.Forbidden => StatusCodes.Status403Forbidden,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.TooManyRequests => StatusCodes.Status429TooManyRequests,
        ErrorType.ExternalService => StatusCodes.Status502BadGateway,
        ErrorType.Unavailable => StatusCodes.Status503ServiceUnavailable,
        ErrorType.Timeout => StatusCodes.Status504GatewayTimeout,
        _ => StatusCodes.Status500InternalServerError, // Unexpected / Custom sin StatusCode
    };

    /// <summary>
    /// Maps a status code to its (Type, Title) per RFC 9110 — same values ASP.NET Core uses internally.
    /// Status codes outside this explicit list (reachable via Error.Custom) fall back to "about:blank" for
    /// Type, with Title taken from the standard IANA reason phrase for that code.
    /// </summary>
    public static (string Type, string Title) ToProblemTypeAndTitle(this int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => ("https://tools.ietf.org/html/rfc9110#section-15.5.1", "Bad Request"),
        StatusCodes.Status401Unauthorized => ("https://tools.ietf.org/html/rfc9110#section-15.5.2", "Unauthorized"),
        StatusCodes.Status403Forbidden => ("https://tools.ietf.org/html/rfc9110#section-15.5.4", "Forbidden"),
        StatusCodes.Status404NotFound => ("https://tools.ietf.org/html/rfc9110#section-15.5.5", "Not Found"),
        StatusCodes.Status409Conflict => ("https://tools.ietf.org/html/rfc9110#section-15.5.10", "Conflict"),
        StatusCodes.Status429TooManyRequests => ("https://tools.ietf.org/html/rfc6585#section-4", "Too Many Requests"),
        StatusCodes.Status500InternalServerError => ("https://tools.ietf.org/html/rfc9110#section-15.6.1", "Internal Server Error"),
        StatusCodes.Status502BadGateway => ("https://tools.ietf.org/html/rfc9110#section-15.6.3", "Bad Gateway"),
        StatusCodes.Status503ServiceUnavailable => ("https://tools.ietf.org/html/rfc9110#section-15.6.4", "Service Unavailable"),
        StatusCodes.Status504GatewayTimeout => ("https://tools.ietf.org/html/rfc9110#section-15.6.5", "Gateway Timeout"),
        _ => ("about:blank", ReasonPhrases.GetReasonPhrase(statusCode) is { Length: > 0 } phrase ? phrase : $"Status Code {statusCode}"),
    };

    /// <summary>
    /// Wraps a business <see cref="Error"/> in ProblemDetails per RFC 9457: Type/Title come from
    /// <see cref="ToProblemTypeAndTitle"/>, Detail is this error's specific message, and the app's own
    /// error code (<see cref="Error.Code"/>) goes in Extensions (not Title, which isn't the place for a
    /// machine-readable identifier).
    /// </summary>
    public static ProblemDetails ToProblemDetails(this Error error, int statusCode)
    {
        var (type, title) = statusCode.ToProblemTypeAndTitle();

        return new ProblemDetails
        {
            Type = type,
            Title = title,
            Status = statusCode,
            Detail = error.Description,
            Extensions = { ["errorCode"] = error.Code },
        };
    }
}
