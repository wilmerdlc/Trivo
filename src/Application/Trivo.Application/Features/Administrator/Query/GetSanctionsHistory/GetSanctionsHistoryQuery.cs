using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Administrator.Query.GetSanctionsHistory;

/// <param name="UserId">Optional: only the sanctions of this user.</param>
public sealed record GetSanctionsHistoryQuery(Guid? UserId, int PageNumber, int PageSize)
    : IQuery<PagedResult<SanctionDto>>;
