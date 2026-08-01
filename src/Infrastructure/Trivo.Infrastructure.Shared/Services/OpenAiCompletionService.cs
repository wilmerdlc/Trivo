using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using Trivo.Application.Interfaces.Services;
using Trivo.Domain.Configurations;

namespace Trivo.Infrastructure.Shared.Services;

public sealed class OpenAiCompletionService : IAiCompletionService
{
    private readonly ChatClient _chatClient;
    private readonly string _model;
    private readonly ILogger<OpenAiCompletionService> _logger;

    public OpenAiCompletionService(IOptions<AiSetting> aiSetting, ILogger<OpenAiCompletionService> logger)
    {
        _model = aiSetting.Value.Model;
        _chatClient = new ChatClient(model: _model, apiKey: aiSetting.Value.ApiKey);
        _logger = logger;
    }

    public async Task<string?> GetCompletionAsync(IEnumerable<AiChatMessage> messages, CancellationToken cancellationToken = default)
    {
        var chatMessages = messages.Select(ToOpenAiMessage).ToList();

        _logger.LogInformation("Requesting OpenAI completion using model '{Model}'.", _model);

        var completion = await _chatClient.CompleteChatAsync(chatMessages, cancellationToken: cancellationToken);

        var content = completion.Value.Content.Count > 0 ? completion.Value.Content[0].Text : null;

        if (string.IsNullOrWhiteSpace(content))
        {
            _logger.LogWarning("OpenAI returned an empty completion for model '{Model}'.", _model);
        }

        return content;
    }

    private static ChatMessage ToOpenAiMessage(AiChatMessage message) => message.Role switch
    {
        AiChatRole.System => new SystemChatMessage(message.Content),
        AiChatRole.User => new UserChatMessage(message.Content),
        _ => throw new ArgumentOutOfRangeException(nameof(message), message.Role, "Unsupported chat role.")
    };
}
