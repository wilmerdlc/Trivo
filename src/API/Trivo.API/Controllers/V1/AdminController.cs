using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trivo.Application.Features.Administrator.Commands.CreateAdministrator;
using Trivo.Application.Features.Administrator.Commands.LoginAdmin;
using Trivo.Application.Features.Administrator.Commands.UnbanUser;
using Trivo.API.Controllers.V1.Requests;
using Trivo.Application.Features.Administrator.Query.GetActiveUsersCount;
using Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount;
using Trivo.Application.Features.Administrator.Query.GetBannedUsersPaged;
using Trivo.Application.Features.Administrator.Query.GetExpertsPaged;
using Trivo.Application.Features.Administrator.Query.GetLastBannedUsers;
using Trivo.Application.Features.Administrator.Query.GetLatestMatches;
using Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged;
using Trivo.Application.Features.Administrator.Query.GetRecruitersPaged;
using Trivo.Application.Features.Administrator.Query.GetReportedUsersCount;
using Trivo.Application.Features.Administrator.Query.GetSanctionsHistory;
using Trivo.Application.Features.Reports.Commands.ResolveReport;
using Trivo.Application.Features.Reports.Query.GetLatestReports;
using Trivo.Application.Features.Reports.Query.GetReportById;
using Trivo.Application.Features.Reports.Query.GetReportsPaged;
using Trivo.Application.Helpers;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;
using Trivo.Application.DTOs.Authentication;
using Trivo.Application.DTOs.Reports;
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
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Reported users count",
        Description = "Gets the total number of users who have been reported on the platform."
    )]
    public async Task<ResultT<ReportedUsersCountDto>> GetReportedUsersCountAsync(
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetReportedUsersCountQuery(), cancellationToken);
    }

    [HttpGet("banned-users/paged")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Banned users (paginated)",
        Description = "Gets a paginated list of banned users, each with its creation date."
    )]
    public async Task<ResultT<PagedResult<UserDto>>> GetBannedUsersPagedAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetBannedUsersPagedQuery(pageNumber, pageSize), cancellationToken);
    }

    [HttpGet("recruiters")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Registered recruiters",
        Description = "Gets a paginated list of registered recruiters, newest first."
    )]
    public async Task<ResultT<PagedResult<AdminRecruiterDto>>> GetRecruitersAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetRecruitersPagedQuery(pageNumber, pageSize), cancellationToken);
    }

    [HttpGet("experts")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Registered experts",
        Description = "Gets a paginated list of registered experts, newest first."
    )]
    public async Task<ResultT<PagedResult<AdminExpertDto>>> GetExpertsAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetExpertsPagedQuery(pageNumber, pageSize), cancellationToken);
    }

    [HttpGet("reports")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Reports (paginated)",
        Description = "Gets a paginated list of reports, newest first. Optionally filter by status (Pending, Approved, Rejected)."
    )]
    public async Task<ResultT<PagedResult<ReportListItemDto>>> GetReportsAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetReportsPagedQuery(pageNumber, pageSize, status), cancellationToken);
    }

    [HttpGet("reports/latest")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Latest 10 reports",
        Description = "Gets the 10 most recent reports."
    )]
    public async Task<ResultT<IEnumerable<ReportListItemDto>>> GetLatestReportsAsync(
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetLatestReportsQuery(), cancellationToken);
    }

    [HttpGet("reports/{reportId:guid}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Report details",
        Description = "Gets a report with the full details of both users involved, the reported content and, once resolved, the decision and sanction."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultT<ReportDetailDto>> GetReportByIdAsync(
        [FromRoute] Guid reportId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetReportByIdQuery(reportId), cancellationToken);
    }

    [HttpPut("reports/{reportId:guid}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Resolve report",
        Description = "Approves or rejects a pending report. A final reason is always required. Approving also requires a sanction type (Warning, TemporarySuspension with durationDays, or PermanentBan). Rejecting notifies the reporter; approving notifies the sanctioned user."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ResultT<ReportResolutionDto>> ResolveReportAsync(
        [FromRoute] Guid reportId,
        [FromBody] ResolveReportRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ResolveReportCommand(
            reportId,
            HttpContext.GetUserId(),
            request.Decision,
            request.FinalReason,
            request.SanctionType,
            request.DurationDays,
            request.NotifyByEmail);

        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("sanctions")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(
        Summary = "Sanction history",
        Description = "Gets the paginated history of administrative sanctions, newest first. Optionally filter by user."
    )]
    public async Task<ResultT<PagedResult<SanctionDto>>> GetSanctionsAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        [FromQuery] Guid? userId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetSanctionsHistoryQuery(userId, pageNumber, pageSize), cancellationToken);
    }
}
