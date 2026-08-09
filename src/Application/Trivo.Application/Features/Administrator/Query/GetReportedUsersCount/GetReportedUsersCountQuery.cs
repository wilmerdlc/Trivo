using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetReportedUsersCount;

public sealed record GetReportedUsersCountQuery()
    : IQuery<ReportedUsersCountDto>;