using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.UpdateProfilePicture;

public sealed class UpdateProfilePictureValidator : AbstractValidator<UpdateProfilePictureCommand>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];

    public UpdateProfilePictureValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID is required.");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("The image is required.")
            .Must(file => file!.Length > 0).WithMessage("The image cannot be empty.")
            .Must(file => AllowedExtensions.Contains(Path.GetExtension(file!.FileName).ToLower()))
            .WithMessage("Only .jpg, .jpeg, .png, or .webp formats are allowed.");
    }
}
