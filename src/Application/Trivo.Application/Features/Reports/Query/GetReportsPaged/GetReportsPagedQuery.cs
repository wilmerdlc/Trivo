using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Query.GetReportsPaged;

/// <param name="Status">Optional filter: "Pending", "Approved" or "Rejected".</param>
public sealed record GetReportsPagedQuery(
    int PageNumber,
    int PageSize,
    string? Status
) : IQuery<PagedResult<ReportListItemDto>>;
