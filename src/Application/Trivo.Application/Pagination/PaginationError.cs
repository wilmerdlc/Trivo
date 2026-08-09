using Trivo.Application.Utils;

namespace Trivo.Application.Pagination;

public static class PaginationError
{
    public static readonly Error InvalidParameters =
        Error.Conflict("409", "Pagination parameters must be greater than zero.");
}