using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.Features.Matching.Commands.CreateMatch;

internal sealed class CreateMatchingCommandHandler(
    ILogger<CreateMatchingCommandHandler> logger,
    IMatchRepository matchingRepository,
    IRecruiterRepository recruiterRepository,
    IExpertRepository expertRepository,
    IUserRepository userRepository,
    IMatchNotifier matchingNotifier,
    ICacheService cache,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateMatchingCommand, MatchDetailsDto>
{
    public async Task<ResultT<MatchDetailsDto>> Handle(CreateMatchingCommand request,
        CancellationToken cancellationToken)
    {
        var recruiter = await recruiterRepository.GetByIdAsync(request.RecruiterId!.Value, cancellationToken);
        if (recruiter is null)
        {
            logger.LogWarning("Recruiter with ID {RecruiterId} was not found.", request.RecruiterId);

            return ResultT<MatchDetailsDto>.Failure(Error.NotFound("404", "The specified recruiter was not found."));
        }

        var expert = await expertRepository.GetByIdAsync(request.ExpertId!.Value, cancellationToken);
        if (expert is null)
        {
            logger.LogWarning("Expert with ID {ExpertId} was not found.", request.ExpertId);

            return ResultT<MatchDetailsDto>.Failure(Error.NotFound("404", "The specified expert was not found."));
        }

        var recruiterUserStatus = await userRepository.GetStatusAsync(recruiter.UserId!.Value, cancellationToken);
        var expertUserStatus = await userRepository.GetStatusAsync(expert.UserId!.Value, cancellationToken);
        if (recruiterUserStatus == UserStatus.Banned.ToString() || expertUserStatus == UserStatus.Banned.ToString())
        {
            logger.LogWarning("Attempted to match a banned user. Recruiter {RecruiterId} (status {RecruiterStatus}), Expert {ExpertId} (status {ExpertStatus}).",
                recruiter.Id, recruiterUserStatus, expert.Id, expertUserStatus);

            return ResultT<MatchDetailsDto>.Failure(Error.Failure("400", "This match cannot be created — one of the users is banned."));
        }

        var existingMatch = await matchingRepository.GetAsync(expert.Id, recruiter.Id, cancellationToken);
        if (existingMatch is not null && existingMatch.MatchStatus != MatchStatus.Rejected.ToString())
        {
            logger.LogWarning("A match already exists between recruiter {RecruiterId} and expert {ExpertId} with status {Status}.",
                recruiter.Id, expert.Id, existingMatch.MatchStatus);

            return ResultT<MatchDetailsDto>.Failure(Error.Conflict("409", "A match already exists between these users."));
        }

        if (!StatusByRole.TryGetValue(request.CreatedBy!.Value, out var value))
        {
            logger.LogWarning("The creator role is invalid. Role: {CreatorRole}.", request.CreatedBy);

            return ResultT<MatchDetailsDto>.Failure(Error.Failure("400", "Invalid creator role."));
        }

        var (expertStatus, recruiterStatus) = value;

        var matching = new Domain.Models.Match
        {
            Id = Guid.NewGuid(),
            RecruiterId = recruiter.Id,
            ExpertId = expert.Id,
            MatchStatus = MatchStatus.Pending.ToString(),
            ExpertStatus = expertStatus,
            RecruiterStatus = recruiterStatus
        };

        await matchingRepository.CreateAsync(matching, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await cache.InvalidateByTagsAsync([CacheKeys.AdminMatchesTag], cancellationToken);

        logger.LogInformation("A new match was created between recruiter {RecruiterId} and expert {ExpertId}.",
            matching.RecruiterId, matching.ExpertId);

        var savedMatching = await matchingRepository.GetDetailsByIdAsync(matching.Id, cancellationToken);

        var mappedExpert = MatchMapper.MapToExpertDto(savedMatching!.Expert!.User!, expert);
        var mappedRecruiter = MatchMapper.MapToRecruiterDto(savedMatching.Recruiter!.User!, recruiter);

        await matchingNotifier.NotifyMatchAsync(
            recruiter.Id,
            expert.Id,
            new List<MatchDto> { MatchMapper.MapToMatchDtoForRecruiter(savedMatching, mappedExpert, mappedRecruiter) },
            new List<MatchDto> { MatchMapper.MapToMatchDtoForExpert(savedMatching, mappedExpert, mappedRecruiter) }
        );

        return ResultT<MatchDetailsDto>.Success(MatchMapper.MapToMatchDetailsDto(savedMatching));
    }

    #region Private Methods

    private static readonly Dictionary<Roles, (string expertStatus, string recruiterStatus)> StatusByRole =
        new()
        {
            { Roles.Expert, (ExpertStatus.Completed.ToString(), RecruiterStatus.Pending.ToString()) },
            { Roles.Recruiter, (ExpertStatus.Pending.ToString(), RecruiterStatus.Completed.ToString()) }
        };

    #endregion
}