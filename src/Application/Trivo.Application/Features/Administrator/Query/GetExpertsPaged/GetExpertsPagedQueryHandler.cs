using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetExpertsPaged;

internal sealed class GetExpertsPagedQueryHandler(
    IAdministratorRepository adminRepository,
    ILogger<GetExpertsPagedQueryHandler> logger
) : IQueryHandler<GetExpertsPagedQuery, PagedResult<AdminExpertDto>>
{
    public async Task<ResultT<PagedResult<AdminExpertDto>>> Handle(
        GetExpertsPagedQuery request,
        CancellationToken cancellationToken)
    {
        var page = await adminRepository.GetPagedExpertsAsync(request.PageNumber, request.PageSize, cancellationToken);

        var items = page.Items!
            .Select(e => new AdminExpertDto(
                ExpertId: e.Id,
                UserId: e.UserId,
                FirstName: e.User?.FirstName,
                LastName: e.User?.LastName,
                Username: e.User?.Username,
                Email: e.User?.Email,
                Position: e.User?.Position,
                AvailableForProjects: e.AvailableForProjects,
                IsHired: e.IsHired,
                UserStatus: e.User?.UserStatus,
                CreatedAt: e.CreatedAt))
            .ToList();

        logger.LogInformation(
            "Experts retrieved. Page={Page} Size={Size} Total={Total}",
            request.PageNumber, request.PageSize, page.TotalItems);

        return ResultT<PagedResult<AdminExpertDto>>.Success(
            new PagedResult<AdminExpertDto>(items, page.TotalItems, request.PageNumber, request.PageSize)
        );
    }
}
