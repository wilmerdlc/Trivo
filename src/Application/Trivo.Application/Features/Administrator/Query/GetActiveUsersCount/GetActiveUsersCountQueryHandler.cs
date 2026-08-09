using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetActiveUsersCount;

internal sealed class GetActiveUsersCountQueryHandler(
    ILogger<GetActiveUsersCountQueryHandler> logger,
    IAdministratorRepository adminRepository
) : IQueryHandler<GetActiveUsersCountQuery, ActiveUsersCountDto>
{
    public async Task<ResultT<ActiveUsersCountDto>> Handle(
        GetActiveUsersCountQuery request,
        CancellationToken cancellationToken)
    {
        var count = await adminRepository.GetCountActiveUsersAsync(cancellationToken);

        logger.LogInformation("Active users count retrieved: {Count}", count);

        return ResultT<ActiveUsersCountDto>.Success(
            new ActiveUsersCountDto(count)
        );
    }
}