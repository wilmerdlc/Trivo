namespace Trivo.API.Controllers.V1.Requests;

public sealed record FilterUsersByInterestsAndSkillsRequest(List<Guid> SkillIds, List<Guid> InterestIds);
