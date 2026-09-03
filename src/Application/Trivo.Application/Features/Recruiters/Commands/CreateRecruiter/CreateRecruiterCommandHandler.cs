using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Application.DTOs.Recruiter;

namespace Trivo.Application.Features.Recruiters.Commands.CreateRecruiter;

internal sealed class CreateRecruiterCommandHandler(
    IRecruiterRepository recruiterRepository,
    IExpertRepository expertRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateRecruiterCommandHandler> logger
) : ICommandHandler<CreateRecruiterCommand, RecruiterDto>
{
    public async Task<ResultT<RecruiterDto>> Handle(CreateRecruiterCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("Received a null CreateRecruiterCommand.");
            return ResultT<RecruiterDto>.Failure(Error.Failure("400", "The request cannot be null."));
        }

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogError("The user with id {UserId} does not exist.", request.UserId);

            return ResultT<RecruiterDto>.Failure(Error.NotFound("404", "The user does not exist"));
        }

        var existingRecruiter = await recruiterRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (existingRecruiter is not null)
        {
            logger.LogWarning("User {UserId} already has a recruiter profile.", request.UserId);

            return ResultT<RecruiterDto>.Failure(Error.Conflict("409", "You already have a recruiter profile."));
        }

        var existingExpert = await expertRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (existingExpert is not null)
        {
            logger.LogWarning("User {UserId} already has an expert profile; cannot also become a recruiter.", request.UserId);

            return ResultT<RecruiterDto>.Failure(Error.Conflict("409", "You already have an expert profile — a user can only be an expert or a recruiter, not both."));
        }

        var recruiter = request.ToRecruiterEntity(Guid.NewGuid());

        await recruiterRepository.CreateAsync(recruiter, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Recruiter '{RecruiterId}' created successfully for user '{UserId}'.", recruiter.Id, request.UserId);

        return ResultT<RecruiterDto>.Success(recruiter.ToRecruiterDto());
    }
}
