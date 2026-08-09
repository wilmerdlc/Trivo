using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trivo.Application.Features.Administrator.Commands.BanUser;
using Trivo.Application.Features.Administrator.Commands.CreateAdministrator;
using Trivo.Application.Features.Administrator.Commands.LoginAdmin;
using Trivo.Application.Features.Administrator.Commands.UnbanUser;
using Trivo.Application.Features.Administrator.Query.GetActiveUsersCount;
using Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount;
using Trivo.Application.Features.Administrator.Query.GetLastBannedUsers;
using Trivo.Application.Features.Administrator.Query.GetLatestMatches;
using Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged;
using Trivo.Application.Features.Administrator.Query.GetReportedUsersCount;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;
using Trivo.Application.DTOs.Authentication;
using Trivo.Application.DTOs.Users;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/admin")]
public class AdminController(ISender sender) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create administrator",
        Description = "Creates a new administrator in the system."
    )]
    public async Task<ResultT<AdminDto>> CreateAdminAsync(
        [FromForm] CreateAdminCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPost("auth")]
    [SwaggerOperation(
        Summary = "Authenticate administrator",
        Description = "Logs in an administrator using their credentials."
    )]
    public async Task<ResultT<TokenResponseDto>> LoginAsync(
        [FromBody] AdminLoginCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("count/match-complete")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Completed matches count",
        Description = "Gets the total count of matches that have been completed."
    )]
    public async Task<ResultT<CompletedMatchesCountDto>> GetCompletedMatchesCountAsync(
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetCompletedMatchesCountQuery(), cancellationToken);
    }

    [HttpGet("count/user-active")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Active users count",
        Description = "Gets the total number of active users on the platform."
    )]
    public async Task<ResultT<ActiveUsersCountDto>> GetActiveUsersCountAsync(
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetActiveUsersCountQuery(), cancellationToken);
    }

    [HttpGet("last-user")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Latest registered users",
        Description = "Gets a paginated list of the most recently registered users."
    )]
    public async Task<ResultT<PagedResult<UserDto>>> GetLatestUsersAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetLatestUsersPagedQuery(pageNumber, pageSize), cancellationToken);
    }

    [HttpGet("banned-users")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Last 10 banned users",
        Description = "Gets the last 10 users who have been banned."
    )]
    public async Task<ResultT<IEnumerable<UserDto>>> GetLastBannedUsersAsync(
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetLast10BannedUsersQuery(), cancellationToken);
    }

    [HttpPut("users/{userId}/ban")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Ban user",
        Description = "Bans the user specified by their ID."
    )]
    public async Task<ResultT<string>> BanUserAsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new BanUserCommand(userId), cancellationToken);
    }

    [HttpPut("users/{userId}/unban")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Unban user",
        Description = "Unbans a previously banned user."
    )]
    public async Task<ResultT<string>> UnbanUserAsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new UnbanUserCommand(userId), cancellationToken);
    }

    [HttpGet("last-match")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Latest matches",
        Description = "Gets the most recent matches, paginated."
    )]
    public async Task<ResultT<PagedResult<AdminMatchDto>>> GetLatestMatchesAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetLatestMatchesQuery(pageNumber, pageSize), cancellationToken);
    }

    [HttpGet("count/users-report")]
    [SwaggerOperation(
        Summary = "Reported users count",
        Description = "Gets the total number of users who have been reported on the platform."
    )]
    public async Task<ResultT<ReportedUsersCountDto>> GetReportedUsersCountAsync(
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetReportedUsersCountQuery(), cancellationToken);
    }
}
