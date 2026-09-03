using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.Features.Matching.Commands.UpdateMatch;

internal sealed class UpdateMatchingCommandHandler(
    ILogger<UpdateMatchingCommandHandler> logger,
    IMatchRepository matchRepository,
    IMatchNotifier matchNotifier,
    ICacheService cache,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateMatchingCommand, MatchDetailsDto>
{
    public async Task<ResultT<MatchDetailsDto>> Handle(UpdateMatchingCommand request, CancellationToken cancellationToken)
    {
        if (request.MatchingId == Guid.Empty)
        {
            logger.LogWarning("An empty MatchingId (Guid.Empty) was received in the request");

            return ResultT<MatchDetailsDto>.Failure(
                Error.Failure("400", "The matching ID is required and cannot be empty"));
        }

        var match = await matchRepository.GetByIdAsync(request.MatchingId, cancellationToken);
        if (match is null)
        {
            logger.LogWarning("Match with ID {MatchingId} not found.", request.MatchingId);
            return ResultT<MatchDetailsDto>.Failure(Error.NotFound("404", "The match does not exist"));
        }

        if (request.MissingByMatching is MissingByMatching.Expert or MissingByMatching.Recruiter)
        {
            await matchRepository.UpdateStatusAsync(request.MatchingId, request.Status, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await cache.InvalidateByTagsAsync([CacheKeys.AdminMatchesTag], cancellationToken);

            var dto = MatchMapper.MapToMatchDetailsDto(match);

            if (request.Status is MatchUpdateStatus.Completed or MatchUpdateStatus.Rejected)
            {
                await NotifyByStatusAsync(request.UserId, match.Id, dto, request.Status.Value);
            }

            logger.LogInformation("Match {MatchingId} updated by {MissingBy}: new status {Status}",
                match.Id, request.MissingByMatching, request.Status);

            return ResultT<MatchDetailsDto>.Success(dto);
        }

        var resultDto = MatchMapper.MapToMatchDetailsDto(match);

        return ResultT<MatchDetailsDto>.Success(resultDto);
    }

    #region Private methods

    private Task NotifyByStatusAsync(Guid userId, Guid matchId, MatchDetailsDto dto, MatchUpdateStatus status)
    {
        return status switch
        {
            MatchUpdateStatus.Completed => matchNotifier.NotifyMatchCompletedAsync(userId, matchId, dto),
            MatchUpdateStatus.Rejected  => matchNotifier.NotifyMatchRejectedAsync(userId, matchId, dto),
            _                              => Task.CompletedTask
        };
    }

    #endregion
}