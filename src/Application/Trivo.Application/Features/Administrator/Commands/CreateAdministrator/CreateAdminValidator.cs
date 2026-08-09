using FluentValidation;

namespace Trivo.Application.Features.Administrator.Commands.CreateAdministrator;

public sealed class CreateAdminValidator : AbstractValidator<CreateAdminCommand>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];

    public CreateAdminValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(30).WithMessage("First name must not exceed 30 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(30).WithMessage("Last name must not exceed 30 characters.");

        RuleFor(x => x.Biography)
            .NotEmpty().WithMessage("Biography is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address must be provided.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

        When(x => x.Photo != null, () =>
        {
            RuleFor(x => x.Photo)
                .Must(file => file!.Length > 0)
                .WithMessage("Image cannot be empty.")
                .Must(file => AllowedExtensions.Contains(
                    Path.GetExtension(file!.FileName).ToLower()))
                .WithMessage("Only .jpg, .jpeg, .png, or .webp formats are allowed.")
                .Must(file => file!.Length <= 5 * 1024 * 1024)
                .WithMessage("Image must not exceed 5 MB.");
        });
    }
}