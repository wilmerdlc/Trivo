using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.InterestCategories;

namespace Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories;

public sealed record GetPaginatedInterestCategoriesQuery(
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<InterestCategoryDto>>;