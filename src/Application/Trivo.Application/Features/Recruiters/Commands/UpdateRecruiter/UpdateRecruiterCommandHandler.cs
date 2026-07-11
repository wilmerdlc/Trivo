using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Recruiter;

namespace Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter;

internal sealed class UpdateRecruiterCommandHandler(
    IRecruiterRepository recruiterRepository,
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

        recruiter.CompanyName = request.CompanyName;

        await recruiterRepository.UpdateAsync(recruiter, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Recruiter '{RecruiterId}' updated successfully.", recruiter.Id);

        return ResultT<RecruiterDto>.Success(recruiter.ToRecruiterDto());
    }
}
