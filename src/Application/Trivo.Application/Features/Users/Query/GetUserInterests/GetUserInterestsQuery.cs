using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.Features.Users.Query.GetUserInterests;

public sealed record GetUserInterestsQuery(Guid UserId) : IQuery<IEnumerable<InterestWithIdDto>>, IUserOwnedRequest;
