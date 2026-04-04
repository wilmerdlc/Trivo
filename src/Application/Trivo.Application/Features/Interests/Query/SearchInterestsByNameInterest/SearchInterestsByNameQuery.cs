using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.Features.Interests.Query.SearchInterestsByNameInterest;

public sealed record SearchInterestsByNameQuery(
    string Name
) : IQuery<IEnumerable<InterestWithIdDto>>;