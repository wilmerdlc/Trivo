using FluentValidation;
using Trivo.Application.Interfaces.Repository;

namespace Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory;

public class CreateInterestCategoryCommandValidator : AbstractValidator<CreateInterestCategoryCommand>
{
    public CreateInterestCategoryCommandValidator(IInterestCategoryRepository repository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.")
            .MustAsync(async (name, ct) => !await repository.NameExistsAsync(name, ct))
            .WithMessage("This name is already registered.");
    }
}