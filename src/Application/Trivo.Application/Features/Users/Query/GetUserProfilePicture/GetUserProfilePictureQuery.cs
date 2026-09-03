using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUserProfilePicture;

public sealed record GetUserProfilePictureQuery(Guid UserId) : IQuery<UserProfilePictureDto>, IUserOwnedRequest;
