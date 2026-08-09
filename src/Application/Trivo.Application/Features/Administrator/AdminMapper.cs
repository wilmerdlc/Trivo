
using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator;

public static class AdminMapper
{
    public static AdminDto ToDto(Domain.Models.Administrator admin)
    {
        return new AdminDto(
            AdminId: admin.Id,
            FirstName: admin.FirstName,
            LastName: admin.LastName,
            Biography: admin.Biography,
            Email: admin.Email,
            Username: admin.Username,
            ProfilePhotoUrl: admin.ProfilePicture,
            RegisteredAt: admin.CreatedAt
        );
    }
}