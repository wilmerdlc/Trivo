using FluentValidation;

namespace Trivo.Application.Features.Messages.Commands.SendMessage;

public sealed class SendMessageValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageValidator()
    {
        RuleFor(x => x.ChatId)
            .NotEmpty().WithMessage("Chat ID is required.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Sender ID is required.");

        RuleFor(x => x.ReceiverId)
            .NotEmpty().WithMessage("Receiver ID is required.")
            .NotEqual(x => x.SenderId).WithMessage("Sender and receiver cannot be the same user.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Message content is required.");
    }
}
