using FluentValidation;

namespace Trivo.Application.Features.Chat.Commands.CreateChat;

public sealed class CreateChatValidator : AbstractValidator<CreateChatCommand>
{
    public CreateChatValidator()
    {
        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Sender ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Sender ID must be a valid GUID.");

        RuleFor(x => x.ReceiverId)
            .NotEmpty().WithMessage("Receiver ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Receiver ID must be a valid GUID.")
            .NotEqual(x => x.SenderId).WithMessage("Sender and receiver cannot be the same user.");
    }
}