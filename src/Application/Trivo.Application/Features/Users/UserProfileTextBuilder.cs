using System.Security.Cryptography;
using System.Text;
using Trivo.Domain.Models;

namespace Trivo.Application.Features.Users;

/// <summary>
/// Builds the plain-text block that represents a user's profile for embedding generation.
/// Deterministic and side-effect free so it can be unit tested without a network call.
/// </summary>
public static class UserProfileTextBuilder
{
    public static string Build(User user)
    {
        var interests = FormatNames(user.UserInterests?.Select(i => i.Interest?.Name));
        var skills = FormatNames(user.UserSkills?.Select(s => s.Skill?.Name));

        return $"""
                Posición: {user.Position}
                Biografía: {user.Biography}
                Intereses: {interests}
                Habilidades: {skills}
                """;
    }

    /// <summary>
    /// SHA-256 of the built profile text, hex-encoded. Used to detect whether a profile actually
    /// changed before paying for (and risking the inherent noise of) a new embedding call —
    /// embedding providers aren't perfectly deterministic across separate calls for the same
    /// input text, so re-embedding unchanged text can shuffle a user's ranking for no reason.
    /// </summary>
    public static string Hash(User user) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(Build(user))));

    private static string FormatNames(IEnumerable<string?>? names) =>
        string.Join(", ", names?.Where(n => !string.IsNullOrWhiteSpace(n)) ?? []);
}
