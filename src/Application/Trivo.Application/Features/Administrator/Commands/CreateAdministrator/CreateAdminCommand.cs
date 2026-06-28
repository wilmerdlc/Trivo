using Microsoft.AspNetCore.Http;
using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Commands.CreateAdministrator;

public sealed record CreateAdminCommand(
    string? FirstName,
    string? LastName,
    string? Biography,
    string? Email,
    string? Password,
    string? Username,
    IFormFile? Photo
) : ICommand<AdminDto>;