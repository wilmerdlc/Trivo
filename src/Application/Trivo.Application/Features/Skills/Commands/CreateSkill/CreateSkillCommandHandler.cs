using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.Features.Skills.Commands.CreateSkill;

internal sealed class CreateSkillCommandHandler(
    ILogger<CreateSkillCommandHandler> logger,
    ISkillRepository skillRepository,
    IUserRepository userRepository,
    IUserSkillRepository userSkillRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateSkillCommand, SkillDto>
{
    public async Task<ResultT<SkillDto>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("User with ID {UserId} was not found.", request.UserId);

            return ResultT<SkillDto>.Failure(Error.NotFound("404", "The specified user was not found."));
        }

        if (await skillRepository.NameExistsAsync(request.Name, cancellationToken))
        {
            logger.LogWarning("A skill with the name '{Name}' already exists.", request.Name);

            return ResultT<SkillDto>.Failure(Error.Conflict("409", "The specified skill already exists."));
        }

        var skillId = Guid.NewGuid();
        var skill = request.ToSkillEntity(skillId);
        var userSkill = request.ToUserSkillEntity(skillId);

        await skillRepository.CreateAsync(skill, cancellationToken);
        await userSkillRepository.AddAsync(userSkill, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Skill '{Name}' created with ID {SkillId} and linked to user {UserId}.",
            skill.Name, skill.SkillId, user.Id);

        return ResultT<SkillDto>.Success(skill.ToSkillDto());
    }
}