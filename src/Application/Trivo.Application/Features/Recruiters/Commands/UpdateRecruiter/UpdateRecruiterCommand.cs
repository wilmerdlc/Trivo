using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Recruiter;

namespace Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter;

public sealed record UpdateRecruiterCommand(
    Guid RecruiterId,
    string CompanyName
) : ICommand<RecruiterDto>;
