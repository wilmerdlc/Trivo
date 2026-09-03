using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI.Embeddings;
using Trivo.Application.Interfaces.Services;
using Trivo.Domain.Configurations;

namespace Trivo.Infrastructure.Shared.Services;

public sealed class OpenAiEmbeddingService : IEmbeddingService
{
    private readonly EmbeddingClient _embeddingClient;
    private readonly string _model;
    private readonly ILogger<OpenAiEmbeddingService> _logger;

    public OpenAiEmbeddingService(IOptions<AiSetting> aiSetting, ILogger<OpenAiEmbeddingService> logger)
    {
        _model = aiSetting.Value.EmbeddingModel;
        _embeddingClient = new EmbeddingClient(model: _model, apiKey: aiSetting.Value.ApiKey);
        _logger = logger;
    }

    public async Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Requesting OpenAI embedding using model '{Model}'.", _model);

        var result = await _embeddingClient.GenerateEmbeddingAsync(text, cancellationToken: cancellationToken);

        return result.Value.ToFloats().ToArray();
    }
}
