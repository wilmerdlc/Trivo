using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trivo.Application.Interfaces.Services;
using Trivo.Domain.Configurations;

namespace Trivo.Infrastructure.Shared.Services;

/// <summary>
/// IEmbeddingService implementation for the Gemini API (generativelanguage.googleapis.com).
/// Requests 1536-dimension output via outputDimensionality so it matches the User.ProfileEmbedding
/// column (vector(1536)) without needing a migration — gemini-embedding-001 supports truncating its
/// native 3072-dim output to 3072/1536/768 via Matryoshka Representation Learning.
/// </summary>
public sealed class GoogleGeminiEmbeddingService : IEmbeddingService
{
    private const int OutputDimensionality = 1536;

    private readonly HttpClient _httpClient;
    private readonly string _model;
    private readonly string _apiKey;
    private readonly ILogger<GoogleGeminiEmbeddingService> _logger;

    public GoogleGeminiEmbeddingService(
        HttpClient httpClient,
        IOptions<AiSetting> aiSetting,
        ILogger<GoogleGeminiEmbeddingService> logger)
    {
        _httpClient = httpClient;
        _model = aiSetting.Value.EmbeddingModel;
        _apiKey = aiSetting.Value.ApiKey;
        _logger = logger;
    }

    public async Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Requesting Gemini embedding using model '{Model}'.", _model);

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"v1beta/models/{_model}:embedContent")
        {
            Content = JsonContent.Create(new EmbedContentRequest(
                Model: $"models/{_model}",
                Content: new ContentPart([new TextPart(text)]),
                OutputDimensionality: OutputDimensionality
            ))
        };
        request.Headers.Add("x-goog-api-key", _apiKey);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<EmbedContentResponse>(cancellationToken);

        return result?.Embedding.Values ?? [];
    }

    private sealed record EmbedContentRequest(
        [property: JsonPropertyName("model")] string Model,
        [property: JsonPropertyName("content")] ContentPart Content,
        [property: JsonPropertyName("outputDimensionality")] int OutputDimensionality);

    private sealed record ContentPart([property: JsonPropertyName("parts")] TextPart[] Parts);

    private sealed record TextPart([property: JsonPropertyName("text")] string Text);

    private sealed record EmbedContentResponse([property: JsonPropertyName("embedding")] EmbeddingValues Embedding);

    private sealed record EmbeddingValues([property: JsonPropertyName("values")] float[] Values);
}
