using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount;

internal sealed class GetCompletedMatchesCountQueryHandler(
    ILogger<GetCompletedMatchesCountQueryHandler> logger,
    IAdministratorRepository adminRepository
) : IQueryHandler<GetCompletedMatchesCountQuery, CompletedMatchesCountDto>
{
    public async Task<ResultT<CompletedMatchesCountDto>> Handle(
        GetCompletedMatchesCountQuery request,
        CancellationToken cancellationToken)
    {
        var count = await adminRepository.GetCountCompletedMatchesAsync(cancellationToken);

        if (count == 0)
        {
            logger.LogWarning("No completed matches were found.");

            return ResultT<CompletedMatchesCountDto>.Failure(
                Error.NotFound("404", "No completed matches found.")
            );
        }

        logger.LogInformation("Found {Count} completed matches.", count);

        return ResultT<CompletedMatchesCountDto>.Success(
            new CompletedMatchesCountDto(count)
        );
    }
}