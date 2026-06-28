using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.Features.Matching.Commands.CreateMatchRejection;

internal sealed class CreateMatchRejectionCommandHandler(
    ILogger<CreateMatchRejectionCommandHandler> logger,
    IMatchRepository matchRepository,
    IRecruiterRepository recruiterRepository,
    IExpertRepository expertRepository,
    IMatchNotifier matchNotifier,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateMatchRejectionCommand, string>
{
    public async Task<ResultT<string>> Handle(CreateMatchRejectionCommand request, CancellationToken cancellationToken)
    {
        var recruiter = await recruiterRepository.GetByIdAsync(request.RecruiterId ?? Guid.Empty, cancellationToken);
        if (recruiter is null)
        {
            logger.LogWarning("Recruiter with ID {RecruiterId} was not found.", request.RecruiterId);

            return ResultT<string>.Failure(Error.NotFound("404", "The specified recruiter was not found."));
        }

        var expert = await expertRepository.GetByIdAsync(request.ExpertId ?? Guid.Empty, cancellationToken);
        if (expert is null)
        {
            logger.LogWarning("Expert with ID {ExpertId} was not found.", request.ExpertId);

            return ResultT<string>.Failure(Error.NotFound("404", "The specified expert was not found."));
        }

        if (!StatusByRole.TryGetValue(request.CreatedBy!.Value, out var value))
        {
            logger.LogWarning("The creator role is invalid. Role: {CreatorRole}.", request.CreatedBy);

            return ResultT<string>.Failure(Error.Failure("400", "Invalid creator role."));
        }

        var (expertStatus, recruiterStatus) = value;

        var match = new Domain.Models.Match
        {
            Id = Guid.NewGuid(),
            RecruiterId = recruiter.Id,
            ExpertId = expert.Id,
            MatchStatus = MatchStatus.Rejected.ToString(),
            ExpertStatus = expertStatus,
            RecruiterStatus = recruiterStatus
        };

        await matchRepository.CreateAsync(match, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("The rejection of the match between recruiter {RecruiterId} and expert {ExpertId} was registered.",
            recruiter.Id, expert.Id);

        var savedMatch = await matchRepository.GetByIdAsync(match.Id, cancellationToken);

        var mappedExpert = MatchMapper.MapToExpertDto(savedMatch!.Expert!.User!, expert);
        var mappedRecruiter = MatchMapper.MapToRecruiterDto(savedMatch.Recruiter!.User!, recruiter);

        var matchDtoForExpert = new MatchDto(
            MatchId: savedMatch.Id,
            RecruiterId: null,
            ExpertId: expert.Id,
            MatchStatus: savedMatch.MatchStatus ?? string.Empty,
            ExpertStatus: savedMatch.ExpertStatus ?? string.Empty,
            RecruiterStatus: savedMatch.RecruiterStatus ?? string.Empty,
            CreatedAt: savedMatch.CreatedAt,
            ExpertDto: mappedExpert,
            RecruiterDto: mappedRecruiter
        );

        var matchDtoForRecruiter = new MatchDto(
            MatchId: savedMatch.Id,
            RecruiterId: null,
            ExpertId: expert.Id,
            MatchStatus: savedMatch.MatchStatus ?? string.Empty,
            ExpertStatus: savedMatch.ExpertStatus ?? string.Empty,
            RecruiterStatus: savedMatch.RecruiterStatus ?? string.Empty,
            CreatedAt: savedMatch.CreatedAt,
            ExpertDto: mappedExpert,
            RecruiterDto: mappedRecruiter
        );

        await matchNotifier.NotifyNewMatchAsync(
            recruiter.Id,
            expert.Id,
            new List<MatchDto> { matchDtoForRecruiter },
            new List<MatchDto> { matchDtoForExpert }
        );

        return ResultT<string>.Success("The match has been rejected.");
    }

    #region Private Methods

    private static readonly Dictionary<Roles, (string expertStatus, string recruiterStatus)> StatusByRole =
        new()
        {
            { Roles.Expert,     (ExpertStatus.Rejected.ToString(), RecruiterStatus.Rejected.ToString()) },
            { Roles.Recruiter,  (ExpertStatus.Rejected.ToString(), RecruiterStatus.Rejected.ToString()) }
        };

    #endregion
}