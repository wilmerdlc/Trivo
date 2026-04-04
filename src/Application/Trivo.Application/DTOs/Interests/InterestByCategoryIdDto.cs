namespace Trivo.Application.DTOs.Interests;

public sealed record InterestByCategoryIdDto(
    Guid InterestId,
    string Name
);