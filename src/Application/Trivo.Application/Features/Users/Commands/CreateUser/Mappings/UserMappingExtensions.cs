using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Users.Commands.CreateUser.Mappings;

public static class UserMappingExtensions
{
    public static Domain.Models.User ToEntity(
        this CreateUserCommand command,
        string passwordHash,
        string profilePictureUrl)
    {
        return new Domain.Models.User
        {
            Id = Guid.NewGuid(),
            FirstName = command.FirstName,
            LastName = command.LastName,
            Biography = command.Biography,
            Email = command.Email,
            PasswordHash = passwordHash,
            Username = command.Username,
            Location = command.Location,
            Position = command.Position,
            ProfilePicture = profilePictureUrl,
            IsAccountConfirmed = false,
            UserStatus = UserStatus.Active.ToString()
        };
    }
}
