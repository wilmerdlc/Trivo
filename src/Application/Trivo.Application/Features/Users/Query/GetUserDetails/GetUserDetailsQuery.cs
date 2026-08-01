using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUserDetails;

public sealed record GetUserDetailsQuery(Guid UserId) : IQuery<UserDetailsDto>;
