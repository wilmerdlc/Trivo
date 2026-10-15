using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetAppliedSanctionsCount;

public sealed record GetAppliedSanctionsCountQuery()
    : IQuery<AppliedSanctionsCountDto>;
