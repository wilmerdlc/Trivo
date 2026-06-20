using System.Text.Json.Serialization;
using Trivo.Application.Abstractions.Messages;
using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Matching.Commands.CreateMatchRejection;

public sealed class CreateMatchRejectionCommand : ICommand<string>
{
    public Guid? RecruiterId { get; set; }

    public Guid? ExpertId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Roles? CreatedBy { get; set; }
}