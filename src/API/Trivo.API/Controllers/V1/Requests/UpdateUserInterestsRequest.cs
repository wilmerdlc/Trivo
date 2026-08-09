namespace Trivo.API.Controllers.V1.Requests;

public sealed record UpdateUserInterestsRequest(List<Guid> InterestIds);
