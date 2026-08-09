using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUserDetails;

internal sealed class GetUserDetailsQueryHandler(
    IUserRepository userRepository,
    IExpertRepository expertRepository,
    IRecruiterRepository recruiterRepository,
    ILogger<GetUserDetailsQueryHandler> logger,
    IDistributedCache cache
) : IQueryHandler<GetUserDetailsQuery, UserDetailsDto>
{
    public async Task<ResultT<UserDetailsDto>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var userDetails = await cache.GetOrCreateAsync(
            $"user-details-{request.UserId}",
            async () =>
            {
                var user = await userRepository.GetDetailsByIdAsync(request.UserId, cancellationToken);

                return user is null ? null : UserMapper.MapToUserDetailsDto(user);
            },
            cancellationToken: cancellationToken
        );

        if (userDetails is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<UserDetailsDto>.Failure(Error.NotFound("404", "The user was not found."));
        }

        if (await expertRepository.IsUserExpertAsync(request.UserId, cancellationToken))
        {
            var expertDetails = await cache.GetOrCreateAsync(
                $"user-details-expert-{request.UserId}",
                async () =>
                {
                    var expert = await expertRepository.GetDetailsAsync(request.UserId, cancellationToken);

                    return UserMapper.MapToExpertDetailsDto(userDetails, expert);
                },
                cancellationToken: cancellationToken
            );

            logger.LogInformation("Expert details retrieved successfully for user '{UserId}'.", request.UserId);

            return ResultT<UserDetailsDto>.Success(expertDetails);
        }

        if (await recruiterRepository.IsUserRecruiterAsync(request.UserId, cancellationToken))
        {
            var recruiterDetails = await cache.GetOrCreateAsync(
                $"user-details-recruiter-{request.UserId}",
                async () =>
                {
                    var recruiter = await recruiterRepository.GetDetailsAsync(request.UserId, cancellationToken);

                    return UserMapper.MapToRecruiterDetailsDto(userDetails, recruiter);
                },
                cancellationToken: cancellationToken
            );

            logger.LogInformation("Recruiter details retrieved successfully for user '{UserId}'.", request.UserId);

            return ResultT<UserDetailsDto>.Success(recruiterDetails);
        }

        logger.LogInformation("User details retrieved successfully for user '{UserId}'.", request.UserId);

        return ResultT<UserDetailsDto>.Success(userDetails);
    }
}
