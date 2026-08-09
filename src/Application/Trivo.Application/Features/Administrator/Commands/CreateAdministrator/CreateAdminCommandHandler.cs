using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.Administrator.Commands.CreateAdministrator.Mappings;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Commands.CreateAdministrator;

internal sealed class CreateAdminCommandHandler(
    ILogger<CreateAdminCommandHandler> logger,
    IAdministratorRepository adminRepository,
    ICloudinaryService cloudinaryService,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateAdminCommand, AdminDto>
{
    public async Task<ResultT<AdminDto>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        if (await adminRepository.EmailExistsAsync(request.Email!, cancellationToken))
        {
            logger.LogWarning("Admin creation failed. Email {Email} is already in use.", request.Email);

            return ResultT<AdminDto>.Failure(
                Error.Conflict("Admin.EmailAlreadyExists", "The provided email is already registered.")
            );
        }

        if (await adminRepository.UsernameExistsAsync(request.Username!, cancellationToken))
        {
            logger.LogWarning("Admin creation failed. Username {Username} is already in use.", request.Username);

            return ResultT<AdminDto>.Failure(
                Error.Conflict("Admin.UsernameAlreadyExists", "The username is already registered.")
            );
        }

        string imageUrl = string.Empty;

        if (request.Photo is not null)
        {
            await using var stream = request.Photo.OpenReadStream();

            imageUrl = await cloudinaryService.UploadImageAsync(
                stream,
                request.Photo.FileName,
                cancellationToken);

            logger.LogInformation("Profile image uploaded for admin {Email}", request.Email);
        }

        var admin = request.ToEntity(
            BCrypt.Net.BCrypt.HashPassword(request.Password),
            imageUrl
        );

        await adminRepository.CreateAsync(admin, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Admin created successfully. Id: {AdminId}, Username: {Username}",
            admin.Id,
            admin.Username
        );

        var dto = AdminMapper.ToDto(admin);

        logger.LogInformation("Admin creation process completed for {Username}", admin.Username);

        return ResultT<AdminDto>.Success(dto);
    }
}