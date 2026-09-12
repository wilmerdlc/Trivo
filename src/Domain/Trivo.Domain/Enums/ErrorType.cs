namespace Trivo.Domain.Enums;

public enum ErrorType
{
    Validation,
    NotFound,
    Conflict,
    Unauthorized,
    Forbidden,
    ExternalService,
    Timeout,
    Unavailable,
    TooManyRequests,
    Unexpected,

    /// <summary>El status code real viene del StatusCode explícito del Error, no de este valor.</summary>
    Custom
}