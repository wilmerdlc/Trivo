using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.DTOs.Interests;

public sealed record InterestDetailsDto
(
    Guid InterestId,
    string Name,
    Guid? CategoryId,
    Guid CreatedBy
);