using MediatR;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.Users.Commands.CreateUser.Mappings;
using Trivo.Application.Features.Users.Events;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Email;
using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Commands.CreateUser;

internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUserInterestRepository userInterestRepository,
    ICloudinaryService cloudinaryService,
    ICodeService codeService,
    IEmailService emailService,
    IAiNotifier aiNotifier,
    IPublisher publisher,
    IUnitOfWork unitOfWork,
    ILogger<CreateUserCommandHandler> logger
) : ICommandHandler<CreateUserCommand, UserDto>
{
    public async Task<ResultT<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.EmailExistsAsync(request.Email!, cancellationToken))
        {
            logger.LogWarning("User creation failed. Email {Email} is already in use.", request.Email);

            return ResultT<UserDto>.Failure(
                Error.Conflict("User.EmailAlreadyExists", "The provided email is already registered.")
            );
        }

        if (await userRepository.UsernameExistsAsync(request.Username!, cancellationToken))
        {
            logger.LogWarning("User creation failed. Username {Username} is already in use.", request.Username);

            return ResultT<UserDto>.Failure(
                Error.Conflict("User.UsernameAlreadyExists", "The username is already registered.")
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

            logger.LogInformation("Profile image uploaded for user {Email}", request.Email);
        }

        var user = request.ToEntity(
            BCrypt.Net.BCrypt.HashPassword(request.Password),
            imageUrl
        );

        await userRepository.CreateAsync(user, cancellationToken);

        if (request.InterestIds is { Count: > 0 })
        {
            var userInterests = request.InterestIds
                .Select(interestId => new UserInterest { UserId = user.Id, InterestId = interestId })
                .ToList();

            await userInterestRepository.AddRangeAsync(userInterests, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "User created successfully. Id: {UserId}, Username: {Username}",
            user.Id,
            user.Username
        );

        var code = await codeService.GenerateCodeAsync(user.Id, CodeType.AccountConfirmation, cancellationToken);

        if (!code.IsSuccess)
        {
            logger.LogError(
                "Failed to generate the confirmation code for user '{UserId}'. Error: {Error}",
                user.Id,
                code.Error!.Description
            );

            return ResultT<UserDto>.Failure(code.Error!);
        }

        await emailService.SendEmailAsync(
            new EmailResponseDto(
                User: request.Email!,
                Body: EmailTemplate.RegisterUser(user.Username!, code.Value),
                Subject: "Confirm your account"
            )
        );

        await publisher.Publish(new UserProfileChangedEvent(user.Id), cancellationToken);

        var userWithRelationships = await userRepository.GetByIdWithRelationshipsAsync(user.Id, cancellationToken);

        await aiNotifier.NotifyNewRecommendationsAsync(
            user.Id,
            [UserMapper.MapToAiRecommendationDto(userWithRelationships!)]
        );

        logger.LogInformation("User '{UserId}' notified successfully with AI recommendations.", user.Id);

        return ResultT<UserDto>.Success(UserMapper.MapToUserDto(user));
    }
}
