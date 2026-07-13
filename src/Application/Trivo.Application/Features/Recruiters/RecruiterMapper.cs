using Trivo.Application.Features.Recruiters.Commands.CreateRecruiter;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Recruiter;

namespace Trivo.Application.Features.Recruiters;

public static class RecruiterMapper
{
    public static RecruiterDto ToRecruiterDto(this Recruiter recruiter) =>
        new(
            Id: recruiter.Id,
            CompanyName: recruiter.CompanyName ?? string.Empty,
            UserId: recruiter.UserId ?? Guid.Empty
        );

    public static Recruiter ToRecruiterEntity(this CreateRecruiterCommand command, Guid id) =>
        new()
        {
            Id = id,
            CompanyName = command.CompanyName,
            UserId = command.UserId
        };
}
