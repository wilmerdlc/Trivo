using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Administrator.Query.GetLastBannedUsers;

public sealed record GetLast10BannedUsersQuery()
    : IQuery<IEnumerable<UserDto>>;