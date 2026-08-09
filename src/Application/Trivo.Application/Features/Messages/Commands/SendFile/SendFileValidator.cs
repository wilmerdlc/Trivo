using FluentValidation;

namespace Trivo.Application.Features.Messages.Commands.SendFile;

public sealed class SendFileValidator : AbstractValidator<SendFileCommand>
{
    private static readonly string[] AllowedExtensions =
    [
        ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".csv", ".zip", ".rar"
    ];

    public SendFileValidator()
    {
        RuleFor(x => x.ChatId)
            .NotEmpty().WithMessage("Chat ID is required.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Sender ID is required.");

        RuleFor(x => x.ReceiverId)
            .NotEmpty().WithMessage("Receiver ID is required.")
            .NotEqual(x => x.SenderId).WithMessage("Sender and receiver cannot be the same user.");

        RuleFor(x => x.File)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("A file is required.")
            .Must(file => file.Length > 0)
            .WithMessage("File cannot be empty.")
            .Must(file => AllowedExtensions.Contains(
                Path.GetExtension(file.FileName).ToLower()))
            .WithMessage("Only .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .txt, .csv, .zip, or .rar formats are allowed.")
            .Must(file => file.Length <= 10 * 1024 * 1024)
            .WithMessage("File must not exceed 10 MB.");
    }
}
