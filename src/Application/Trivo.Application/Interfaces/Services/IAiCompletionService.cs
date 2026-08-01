namespace Trivo.Application.Interfaces.Services;

public interface IAiCompletionService
{
    Task<string?> GetCompletionAsync(IEnumerable<AiChatMessage> messages, CancellationToken cancellationToken = default);
}
