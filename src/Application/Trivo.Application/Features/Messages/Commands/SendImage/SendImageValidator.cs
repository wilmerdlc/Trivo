using FluentValidation;

namespace Trivo.Application.Features.Messages.Commands.SendImage;

public sealed class SendImageValidator : AbstractValidator<SendImageCommand>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];

    public SendImageValidator()
    {
        RuleFor(x => x.ChatId)
            .NotEmpty().WithMessage("Chat ID is required.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Sender ID is required.");

        RuleFor(x => x.ReceiverId)
            .NotEmpty().WithMessage("Receiver ID is required.")
            .NotEqual(x => x.SenderId).WithMessage("Sender and receiver cannot be the same user.");

        RuleFor(x => x.Image)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("An image is required.")
            .Must(file => file.Length > 0)
            .WithMessage("Image cannot be empty.")
            .Must(file => AllowedExtensions.Contains(
                Path.GetExtension(file.FileName).ToLower()))
            .WithMessage("Only .jpg, .jpeg, .png, or .webp formats are allowed.")
            .Must(file => file.Length <= 5 * 1024 * 1024)
            .WithMessage("Image must not exceed 5 MB.");
    }
}
