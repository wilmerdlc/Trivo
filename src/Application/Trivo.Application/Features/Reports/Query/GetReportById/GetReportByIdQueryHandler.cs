using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Query.GetReportById;

internal sealed class GetReportByIdQueryHandler(
    IReportRepository reportRepository,
    ILogger<GetReportByIdQueryHandler> logger
) : IQueryHandler<GetReportByIdQuery, ReportDetailDto>
{
    public async Task<ResultT<ReportDetailDto>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
    {
        var report = await reportRepository.GetDetailsByIdAsync(request.ReportId, cancellationToken);
        if (report is null)
        {
            logger.LogWarning("No report was found with ID '{ReportId}'.", request.ReportId);

            return ResultT<ReportDetailDto>.Failure(Error.NotFound("404", "Report not found."));
        }

        logger.LogInformation("Report '{ReportId}' retrieved.", report.ReportId);

        return ResultT<ReportDetailDto>.Success(report.ToDetailDto(DateTime.UtcNow));
    }
}
