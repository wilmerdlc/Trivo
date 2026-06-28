using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetActiveUsersCount;

public sealed record GetActiveUsersCountQuery()
    : IQuery<ActiveUsersCountDto>;