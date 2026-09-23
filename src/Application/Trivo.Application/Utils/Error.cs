using Trivo.Domain.Enums;

namespace Trivo.Application.Utils;

/// <summary>
/// Represents an error that occurred during an operation, including a code, description, and type.
/// </summary>
public class Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class with the specified code, description, and error type.
    /// </summary>
    /// <param name="code">A unique identifier for the error.</param>
    /// <param name="description">A human-readable description of the error.</param>
    /// <param name="errorType">The type or category of the error.</param>
    /// <param name="statusCode">
    /// Explicit HTTP status code, set by the caller instead of being derived from <paramref name="errorType"/>.
    /// Escape hatch for status codes no <see cref="ErrorType"/> default covers (402, 422, 410, etc.).
    /// </param>
    private Error(
        string code,
        string description,
        ErrorType errorType,
        int? statusCode = null)
    {
        Code = code;
        Description = description;
        ErrorType = errorType;
        StatusCode = statusCode;
    }

    /// <summary>
    /// Gets the unique code identifying the error.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the description of the error.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the type or category of the error.
    /// </summary>
    public ErrorType ErrorType { get; }

    /// <summary>
    /// Gets the explicit HTTP status code for this error, when set. When <see langword="null"/>, the
    /// status code is derived from <see cref="ErrorType"/> by the API layer.
    /// </summary>
    public int? StatusCode { get; }

    /// <summary>
    /// Optional structured data that travels with the error (e.g. the remaining time of a
    /// suspension). The API layer copies it into the ProblemDetails extensions, so clients get it
    /// as fields instead of having to parse <see cref="Description"/>.
    /// </summary>
    public IReadOnlyDictionary<string, object?>? Extensions { get; private init; }

    /// <summary>
    /// Returns a copy of this error carrying the given structured data.
    /// </summary>
    public Error WithExtensions(IReadOnlyDictionary<string, object?> extensions) =>
        new(Code, Description, ErrorType, StatusCode) { Extensions = extensions };

    /// <summary>
    /// Creates a validation error (input failed validation rules).
    /// </summary>
    public static Error Validation(string code, string description) =>
        new Error(code, description, ErrorType.Validation);

    /// <summary>
    /// Creates a forbidden error (authenticated caller, but not allowed to perform the operation).
    /// </summary>
    public static Error Forbidden(string code, string description) =>
        new Error(code, description, ErrorType.Forbidden);

    /// <summary>
    /// Creates an error for a failed call to an external dependency (HTTP, third-party API, etc.).
    /// </summary>
    public static Error ExternalService(string code, string description) =>
        new Error(code, description, ErrorType.ExternalService);

    /// <summary>
    /// Creates an error for a dependency that did not respond in time.
    /// </summary>
    public static Error Timeout(string code, string description) =>
        new Error(code, description, ErrorType.Timeout);

    /// <summary>
    /// Creates an error for a service or dependency temporarily unavailable.
    /// </summary>
    public static Error Unavailable(string code, string description) =>
        new Error(code, description, ErrorType.Unavailable);

    /// <summary>
    /// Creates an error for a rate limit/throttling threshold being exceeded.
    /// </summary>
    public static Error TooManyRequests(string code, string description) =>
        new Error(code, description, ErrorType.TooManyRequests);

    /// <summary>
    /// Creates an unexpected error without a more specific category.
    /// </summary>
    public static Error Unexpected(string code, string description) =>
        new Error(code, description, ErrorType.Unexpected);

    /// <summary>
    /// Creates a fully custom error: its own HTTP status code and <see cref="ErrorType.Custom"/> as
    /// category — use when the error doesn't fit any predefined <see cref="ErrorType"/>.
    /// </summary>
    public static Error Custom(string code, string description, int statusCode) =>
        new Error(code, description, ErrorType.Custom, statusCode);

    /// <summary>
    /// Creates an error that keeps an existing <see cref="ErrorType"/> category (so logging/business rules
    /// switching on <see cref="ErrorType"/> still see it as such) but overrides its HTTP status code.
    /// </summary>
    public static Error Custom(string code, string description, ErrorType errorType, int statusCode) =>
        new Error(code, description, errorType, statusCode);

    /// <summary>
    /// Creates a resource not found error.
    /// </summary>
    /// <param name="code">The code identifying the error.</param>
    /// <param name="description">A description indicating what was not found.</param>
    /// <returns>An instance of <see cref="Error"/> representing a not found error.</returns>
    public static Error NotFound(string code, string description) =>
        new Error(code, description, ErrorType.NotFound);
    
    /// <summary>
    /// Creates a conflict error, typically used when a resource already exists or there is a data conflict.
    /// </summary>
    /// <param name="code">The code identifying the conflict error.</param>
    /// <param name="description">A description of the conflict.</param>
    /// <returns>An instance of <see cref="Error"/> representing a conflict.</returns>
    public static Error Conflict(string code, string description) =>
        new Error(code, description, ErrorType.Conflict);

    /// <summary>
    /// Creates an unauthorized error.
    /// </summary>
    /// <param name="code">The code identifying the error.</param>
    /// <param name="description">A description of the authorization failure.</param>
    /// <returns>An instance of <see cref="Error"/> representing an unauthorized error.</returns>
    public static Error Unauthorized(string code, string description) =>
        new Error(code, description, ErrorType.Unauthorized);
}