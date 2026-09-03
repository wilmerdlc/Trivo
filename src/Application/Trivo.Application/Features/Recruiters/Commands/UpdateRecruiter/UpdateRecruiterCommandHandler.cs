using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Recruiter;

namespace Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter;

internal sealed class UpdateRecruiterCommandHandler(
    IRecruiterRepository recruiterRepository,
    ICacheService cache,
    IUnitOfWork unitOfWork,
    ILogger<UpdateRecruiterCommandHandler> logger
) : ICommandHandler<UpdateRecruiterCommand, RecruiterDto>
{
    public async Task<ResultT<RecruiterDto>> Handle(UpdateRecruiterCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("Received a null request to update a recruiter.");
            return ResultT<RecruiterDto>.Failure(Error.Failure("400", "The request cannot be null."));
        }

        var recruiter = await recruiterRepository.GetByIdAsync(request.RecruiterId, cancellationToken);
        if (recruiter is null)
        {
            logger.LogError("Recruiter with ID {RecruiterId} was not found.", request.RecruiterId);

            return ResultT<RecruiterDto>.Failure(Error.NotFound("404", "The recruiter does not exist."));
        }

        if (recruiter.UserId != request.RequesterId)
        {
            logger.LogWarning(
                "User {RequesterId} attempted to update recruiter {RecruiterId}, which belongs to a different user.",
                request.RequesterId, request.RecruiterId);

            return ResultT<RecruiterDto>.Failure(Error.Unauthorized("403", "You can only update your own recruiter profile."));
        }

        recruiter.CompanyName = request.CompanyName;

        await recruiterRepository.UpdateAsync(recruiter, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await cache.InvalidateByTagsAsync([CacheKeys.UserTag(recruiter.UserId!.Value)], cancellationToken);

        logger.LogInformation("Recruiter '{RecruiterId}' updated successfully.", recruiter.Id);

        return ResultT<RecruiterDto>.Success(recruiter.ToRecruiterDto());
    }
}
