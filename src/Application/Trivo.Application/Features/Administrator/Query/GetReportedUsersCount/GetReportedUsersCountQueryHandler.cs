using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetReportedUsersCount;

internal sealed class GetReportedUsersCountQueryHandler(
    ILogger<GetReportedUsersCountQueryHandler> logger,
    IAdministratorRepository adminRepository
) : IQueryHandler<GetReportedUsersCountQuery, ReportedUsersCountDto>
{
    public async Task<ResultT<ReportedUsersCountDto>> Handle(
        GetReportedUsersCountQuery request,
        CancellationToken cancellationToken)
    {
        var count = await adminRepository.GetReportedCountAsync(cancellationToken);

        logger.LogInformation("Reported users count retrieved: {Count}", count);

        return ResultT<ReportedUsersCountDto>.Success(
            new ReportedUsersCountDto(count)
        );
    }
}