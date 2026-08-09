using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetCompletedMatchesCount;

public sealed record GetCompletedMatchesCountQuery()
    : IQuery<CompletedMatchesCountDto>;