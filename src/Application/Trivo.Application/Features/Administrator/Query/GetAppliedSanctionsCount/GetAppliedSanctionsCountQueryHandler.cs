using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetAppliedSanctionsCount;

internal sealed class GetAppliedSanctionsCountQueryHandler(
    ILogger<GetAppliedSanctionsCountQueryHandler> logger,
    ISanctionRepository sanctionRepository
) : IQueryHandler<GetAppliedSanctionsCountQuery, AppliedSanctionsCountDto>
{
    public async Task<ResultT<AppliedSanctionsCountDto>> Handle(
        GetAppliedSanctionsCountQuery request,
        CancellationToken cancellationToken)
    {
        var count = await sanctionRepository.GetAppliedCountAsync(cancellationToken);

        logger.LogInformation("Applied sanctions count retrieved: {Count}", count);

        return ResultT<AppliedSanctionsCountDto>.Success(
            new AppliedSanctionsCountDto(count)
        );
    }
}
