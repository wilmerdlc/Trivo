namespace Trivo.Domain.Configurations;

public class AiSetting
{
    /// <summary>
    /// Which embedding provider implementation to register: "OpenAI" or "Gemini".
    /// Defaults to "OpenAI" when unset.
    /// </summary>
    public string Provider { get; set; } = "OpenAI";

    public required string EmbeddingModel { get; set; }

    public required string ApiKey { get; set; }
}
