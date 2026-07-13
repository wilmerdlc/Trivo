using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Recruiter;

namespace Trivo.Application.Features.Recruiters.Commands.CreateRecruiter;

public record CreateRecruiterCommand(
    string CompanyName,
    Guid UserId
) : ICommand<RecruiterDto>;

