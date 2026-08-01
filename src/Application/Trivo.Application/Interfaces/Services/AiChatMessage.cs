namespace Trivo.Application.Interfaces.Services;

public enum AiChatRole
{
    System,
    User
}

public sealed record AiChatMessage(AiChatRole Role, string Content)
{
    public static AiChatMessage System(string content) => new(AiChatRole.System, content);

    public static AiChatMessage User(string content) => new(AiChatRole.User, content);
}
