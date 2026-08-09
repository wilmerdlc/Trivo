namespace Trivo.Application.Features.Administrator.Commands.CreateAdministrator.Mappings;

/// <summary>
/// Contains extension mapping methods for converting command models
/// into domain entities.
///
/// This class belongs to the write side of the application (Commands)
/// and is intended to transform incoming request data into domain objects.
///
/// Responsibility:
/// - Command → Domain transformation
/// - Entity construction
/// - Encapsulated mapping logic
///
/// Architectural Purpose:
/// Keeps handlers clean by removing object construction logic and
/// centralizing mapping responsibility.
///
/// Design Notes:
/// - Extension methods improve readability
/// - Supports Vertical Slice Architecture
/// - Keeps feature-local mapping close to its use case
///
/// Usage Context:
/// Command handlers only.
/// </summary>
public static class AdminMappingExtensions
{
    public static Domain.Models.Administrator ToEntity(
        this CreateAdminCommand command,
        string passwordHash,
        string profilePictureUrl)
    {
        return new Domain.Models.Administrator
        {
            Id = Guid.NewGuid(),
            FirstName = command.FirstName,
            LastName = command.LastName,
            Biography = command.Biography,
            Email = command.Email!,
            PasswordHash = passwordHash,
            Username = command.Username!,
            ProfilePicture = profilePictureUrl
        };
    }
}