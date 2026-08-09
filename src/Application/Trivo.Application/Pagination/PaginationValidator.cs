using Microsoft.Extensions.Logging;
using Trivo.Application.Utils;

namespace Trivo.Application.Pagination;

/// <summary>
/// Provides utility methods for validating pagination parameters across the application.
/// </summary>
public static class PaginationValidator
{
    /// <summary>
    /// Validates that the provided pagination parameters are strictly greater than zero.
    /// </summary>
    /// <typeparam name="T">The type of the expected result payload.</typeparam>
    /// <param name="pageNumber">The current page number requested by the client.</param>
    /// <param name="pageSize">The number of items per page requested by the client.</param>
    /// <param name="logger">The logger instance used to record validation warnings.</param>
    /// <param name="failureResult">When this method returns <c>false</c>, contains the formatted failure result. Otherwise, <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if the pagination parameters are valid; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryValidate<T>(
        int pageNumber,
        int pageSize,
        ILogger logger,
        out ResultT<T> failureResult)
    {
        // Evaluate if the parameters violate the business rule (must be positive integers)
        if (pageNumber <= 0 || pageSize <= 0)
        {
            // Log the warning with structured logging for observability
            logger.LogWarning("Invalid pagination parameters. PageNumber: {PageNumber}, PageSize: {PageSize}",
                pageNumber, pageSize);

            // Populate the out parameter with the standardized domain error
            failureResult = ResultT<T>.Failure(PaginationError.InvalidParameters);

            return false;
        }

        // Assign null-forgiving to the out parameter since the validation succeeded
        failureResult = null!;

        return true;
    }
}