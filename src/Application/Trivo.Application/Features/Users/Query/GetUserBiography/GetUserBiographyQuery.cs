using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUserBiography;

public sealed record GetUserBiographyQuery(Guid UserId) : IQuery<UserBiographyDto>;
