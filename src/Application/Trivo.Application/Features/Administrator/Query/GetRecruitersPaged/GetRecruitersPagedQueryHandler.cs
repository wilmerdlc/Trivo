using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetRecruitersPaged;

internal sealed class GetRecruitersPagedQueryHandler(
    IAdministratorRepository adminRepository,
    ILogger<GetRecruitersPagedQueryHandler> logger
) : IQueryHandler<GetRecruitersPagedQuery, PagedResult<AdminRecruiterDto>>
{
    public async Task<ResultT<PagedResult<AdminRecruiterDto>>> Handle(
        GetRecruitersPagedQuery request,
        CancellationToken cancellationToken)
    {
        var page = await adminRepository.GetPagedRecruitersAsync(request.PageNumber, request.PageSize, cancellationToken);

        var items = page.Items!
            .Select(r => new AdminRecruiterDto(
                RecruiterId: r.Id,
                UserId: r.UserId,
                FirstName: r.User?.FirstName,
                LastName: r.User?.LastName,
                Username: r.User?.Username,
                Email: r.User?.Email,
                CompanyName: r.CompanyName,
                UserStatus: r.User?.UserStatus,
                CreatedAt: r.CreatedAt))
            .ToList();

        logger.LogInformation(
            "Recruiters retrieved. Page={Page} Size={Size} Total={Total}",
            request.PageNumber, request.PageSize, page.TotalItems);

        return ResultT<PagedResult<AdminRecruiterDto>>.Success(
            new PagedResult<AdminRecruiterDto>(items, page.TotalItems, request.PageNumber, request.PageSize)
        );
    }
}
