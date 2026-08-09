using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.Features.Matching.Query.GetMatchByUser;

internal sealed class GetMatchByUserQueryHandler(
    ILogger<GetMatchByUserQueryHandler> logger,
    IMatchRepository matchRepository,
    IUserRepository userRepository,
    IMatchNotifier matchNotifier,
    IExpertRepository expertRepository,
    IRecruiterRepository recruiterRepository
) : IQueryHandler<GetMatchByUserQuery, IEnumerable<MatchDto>>
{
    public async Task<ResultT<IEnumerable<MatchDto>>> Handle(GetMatchByUserQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("The match retrieval request is null.");

            return ResultT<IEnumerable<MatchDto>>.Failure(Error.Failure("400", "The request cannot be null."));
        }

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("User with ID {UserId} was not found.", request.UserId);

            return ResultT<IEnumerable<MatchDto>>.Failure(Error.NotFound("404", "The user does not exist."));
        }

        var recruiter = await recruiterRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        var expert = await expertRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        var matchStrategies = GetStrategies(request.UserId);

        if (!matchStrategies.TryGetValue(request.Role, out var matchFilter))
        {
            logger.LogWarning("The provided role '{Role}' does not have a valid match strategy.", request.Role);

            return ResultT<IEnumerable<MatchDto>>.Failure(Error.Failure("400", "The provided role is not valid for matches."));
        }

        var matches = await matchFilter(cancellationToken);

        List<MatchDto> matchDtos;
        var enumerable = matches.ToList();

        if (request.Role == Roles.Expert)
        {
            matchDtos = enumerable.Select(m =>
                new MatchDto(
                    MatchId: m.Id,
                    RecruiterId: m.Recruiter!.Id,
                    ExpertId: m.Expert!.Id,
                    ExpertStatus: m.ExpertStatus ?? string.Empty,
                    RecruiterStatus: m.RecruiterStatus ?? string.Empty,
                    MatchStatus: m.MatchStatus ?? string.Empty,
                    CreatedAt: m.CreatedAt,
                    ExpertDto: null,
                    RecruiterDto: MatchMapper.MapToRecruiterDto(m.Recruiter!.User!, m.Recruiter!)
                )
            ).ToList();
        }
        else if (request.Role == Roles.Recruiter)
        {
            matchDtos = enumerable.Select(m =>
                new MatchDto(
                    MatchId: m.Id,
                    RecruiterId: m.Recruiter!.Id,
                    ExpertId: m.Expert!.Id,
                    ExpertStatus: m.ExpertStatus ?? string.Empty,
                    RecruiterStatus: m.RecruiterStatus ?? string.Empty,
                    MatchStatus: m.MatchStatus ?? string.Empty,
                    CreatedAt: m.CreatedAt,
                    ExpertDto: MatchMapper.MapToExpertDto(m.Expert!.User!, m.Expert!),
                    RecruiterDto: null
                )
            ).ToList();
        }
        else
        {
            matchDtos = new List<MatchDto>();
        }

        var pagedMatchDtos = matchDtos
            .Paginate(request.PageNumber, request.PageSize)
            .ToList();

        logger.LogInformation("Successfully retrieved {Count} matches for user {UserId} with role {Role}.",
            enumerable.Count, request.UserId, request.Role);

        if (!matchDtos.Any())
        {
            logger.LogWarning("No pending matches were found for user {UserId} with role {Role}.", request.UserId, request.Role);

            return ResultT<IEnumerable<MatchDto>>.Failure(Error.Failure("404", "No matches were found for the user."));
        }

        logger.LogInformation("Successfully retrieved {Count} matches for user {UserId} with role {Role}.",
            enumerable.Count, request.UserId, request.Role);

        if (recruiter is null || expert is null)
        {
            logger.LogInformation("The notification was sent to only one of the users because the other does not apply in this context (recruiter or expert is null).");
        }
        else
        {
            await matchNotifier.NotifyMatchAsync(
                recruiter.Id,
                expert.Id,
                pagedMatchDtos,
                pagedMatchDtos
            );
            logger.LogInformation("Both users (recruiter and expert) were successfully notified via SignalR.");
        }

        return ResultT<IEnumerable<MatchDto>>.Success(pagedMatchDtos);
    }

    #region Private Methods

    private Dictionary<Roles, Func<CancellationToken, Task<IEnumerable<Match>>>> GetStrategies(Guid userId)
    {
        return new Dictionary<Roles, Func<CancellationToken, Task<IEnumerable<Match>>>>
        {
            { Roles.Expert,    async ct => await matchRepository.GetAsExpertAsync(userId, ct) },
            { Roles.Recruiter, async ct => await matchRepository.GetAsRecruiterAsync(userId, ct) }
        };
    }

    #endregion
}