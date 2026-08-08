using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.API.Controllers.V1.Requests;
using Trivo.Application.Features.Users.Commands.ChangePassword;
using Trivo.Application.Features.Users.Commands.ConfirmAccount;
using Trivo.Application.Features.Users.Commands.CreateUser;
using Trivo.Application.Features.Users.Commands.ForgotPassword;
using Trivo.Application.Features.Users.Commands.LoginUser;
using Trivo.Application.Features.Users.Commands.UpdateBiography;
using Trivo.Application.Features.Users.Commands.UpdatePassword;
using Trivo.Application.Features.Users.Commands.UpdateProfilePicture;
using Trivo.Application.Features.Users.Commands.UpdateUser;
using Trivo.Application.Features.Interests.Commands.UpdateInterest;
using Trivo.Application.Features.Skills.Commands.UpdateSkill;
using Trivo.Application.Features.Users.Query.GetUserBiography;
using Trivo.Application.Features.Users.Query.GetUserDetails;
using Trivo.Application.Features.Users.Query.GetUserInterests;
using Trivo.Application.Features.Users.Query.GetUserProfilePicture;
using Trivo.Application.Features.Users.Query.GetUserSkills;
using Trivo.Application.Features.Users.Query.GetUsersByInterestsAndSkills;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Authentication;
using Trivo.Application.DTOs.Interests;
using Trivo.Application.DTOs.Skills;
using Trivo.Application.DTOs.Users;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
public class UserController(
    ISender sender,
    IEmailValidationService emailValidationService,
    ICodeService codeService
) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultT<UserDto>> RegisterUserAsync(
        [FromForm] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPost("confirm-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultT<string>> ConfirmAccountAsync(
        [FromQuery] Guid userId,
        [FromQuery] string code,
        CancellationToken cancellationToken)
    {
        var command = new ConfirmAccountCommand(userId, code);
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("profile/{userId}")]
    [Authorize]
    public async Task<ResultT<UserDetailsDto>> GetUserDetailsAsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserDetailsQuery(userId), cancellationToken);
    }

    [HttpPost("auth")]
    public async Task<ResultT<TokenResponseDto>> LoginAsync(
        [FromBody] LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPost("forgot-password")]
    public async Task<ResultT<string>> ForgotPasswordAsync(
        [FromBody] ForgotPasswordCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPost("modify-password")]
    public async Task<ResultT<string>> ChangePasswordAsync(
        [FromQuery] string email,
        [FromBody] ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangePasswordCommand(email, request.NewPassword, request.ConfirmPassword);
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{userId}/info")]
    [Authorize]
    public async Task<ResultT<UpdateUserDto>> UpdateUserAsync(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserInfoRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(userId, request.Username, request.Email);
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{userId}/profile-photo")]
    [Authorize]
    public async Task<ResultT<string>> UpdateProfilePictureAsync(
        [FromRoute] Guid userId,
        [FromForm] UpdateProfilePictureRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProfilePictureCommand(userId, request.Image);
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("{userId}/profile-photo")]
    [Authorize]
    public async Task<ResultT<UserProfilePictureDto>> GetProfilePictureAsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserProfilePictureQuery(userId), cancellationToken);
    }

    [HttpPut("{userId}/bio")]
    [Authorize]
    public async Task<ResultT<string>> UpdateBiographyAsync(
        [FromRoute] Guid userId,
        [FromBody] UpdateBiographyRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateBiographyCommand(userId, request.Biography);
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("{userId}/bio")]
    [Authorize]
    public async Task<ResultT<UserBiographyDto>> GetBiographyAsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserBiographyQuery(userId), cancellationToken);
    }

    [HttpPut("{userId}/interests")]
    [Authorize]
    public async Task<ResultT<string>> UpdateUserInterestsAsync(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserInterestsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateInterestCommand(userId, request.InterestIds);
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("{userId}/interests")]
    [Authorize]
    public async Task<ResultT<IEnumerable<InterestWithIdDto>>> GetUserInterestsAsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserInterestsQuery(userId), cancellationToken);
    }

    [HttpPut("{userId}/ability")]
    [Authorize]
    public async Task<ResultT<string>> UpdateUserSkillsAsync(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserSkillsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateSkillCommand(userId, request.SkillIds);
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("{userId}/ability")]
    [Authorize]
    public async Task<ResultT<IEnumerable<SkillWithIdDto>>> GetUserSkillsAsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserSkillsQuery(userId), cancellationToken);
    }

    [HttpGet("verify-email")]
    public async Task<ResultT<bool>> VerifyEmailAsync(
        [FromQuery] string email,
        CancellationToken cancellationToken)
    {
        return await emailValidationService.ValidateEmailAsync(email, cancellationToken);
    }

    [HttpPost("filter-by-interests-and-ability")]
    // [Authorize]
    public async Task<ResultT<PagedResult<UserAiRecommendationDto>>> GetUsersByInterestsAndSkillsAsync(
        [FromBody] FilterUsersByInterestsAndSkillsRequest request,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        var query = new GetUsersByInterestsAndSkillsQuery(pageNumber, pageSize, request.SkillIds, request.InterestIds);
        return await sender.Send(query, cancellationToken);
    }

    [HttpPost("validate-code/{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultT<string>> ValidateCodeAsync(
        [FromRoute] string code,
        CancellationToken cancellationToken)
    {
        return await codeService.ValidateCodeAsync(code, cancellationToken);
    }

    [HttpPost("{userId}/change-password")]
    [Authorize]
    public async Task<ResultT<string>> UpdatePasswordAsync(
        [FromRoute] Guid userId,
        [FromBody] UpdatePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePasswordCommand(userId, request.OldPassword, request.NewPassword, request.ConfirmPassword);
        return await sender.Send(command, cancellationToken);
    }
}
