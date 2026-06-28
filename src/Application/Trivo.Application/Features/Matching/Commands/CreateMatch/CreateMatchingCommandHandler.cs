using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
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
    IMatchNotifier matchingNotifier,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateMatchingCommand, MatchDetailsDto>
{
    public async Task<ResultT<MatchDetailsDto>> Handle(CreateMatchingCommand request,
        CancellationToken cancellationToken)
    {
        // This method loads the user's skills and interests entities
        var recruiter = await recruiterRepository.GetByIdAsync(request.RecruiterId!.Value, cancellationToken);
        if (recruiter is null)
        {
            logger.LogWarning("Recruiter with ID {RecruiterId} was not found.", request.RecruiterId);

            return ResultT<MatchDetailsDto>.Failure(Error.NotFound("404", "The specified recruiter was not found."));
        }

        // This method loads the user's skills and interests entities
        var expert = await expertRepository.GetByIdAsync(request.ExpertId!.Value, cancellationToken);
        if (expert is null)
        {
            logger.LogWarning("Expert with ID {ExpertId} was not found.", request.ExpertId);

            return ResultT<MatchDetailsDto>.Failure(Error.NotFound("404", "The specified expert was not found."));
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

        logger.LogInformation("A new match was created between recruiter {RecruiterId} and expert {ExpertId}.",
            matching.RecruiterId, matching.ExpertId);

        var savedMatching = await matchingRepository.GetByIdAsync(matching.Id, cancellationToken);

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