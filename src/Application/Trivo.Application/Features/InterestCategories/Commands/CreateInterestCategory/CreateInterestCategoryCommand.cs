using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.InterestCategories;

namespace Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory;

public sealed record CreateInterestCategoryCommand(string Name) : ICommand<InterestCategoryDto>;