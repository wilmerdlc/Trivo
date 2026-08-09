using Trivo.Application.Interfaces.Services;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

namespace Trivo.Application.Features.Users.Query.GetUserRecommendations;

/// <summary>
/// Builds the chat messages sent to the AI for a user-recommendation request.
/// The system message carries the fixed instructions/output format (stable across requests,
/// which lets providers like OpenAI cache it); the user message carries the request-specific
/// profile and candidate data.
/// </summary>
internal static class UserRecommendationPromptBuilder
{
    public static IReadOnlyList<AiChatMessage> Build(User currentUser, IReadOnlyCollection<User> candidates, string role)
    {
        return
        [
            AiChatMessage.System(BuildSystemPrompt(role)),
            AiChatMessage.User(BuildUserPrompt(currentUser, candidates))
        ];
    }

    private static string BuildSystemPrompt(string role)
    {
        var perspective = role == Roles.Recruiter.ToString()
            ? "Eres un asistente que ayuda a un RECLUTADOR a encontrar EXPERTOS que coincidan con sus requisitos."
            : role == Roles.Expert.ToString()
                ? "Eres un asistente que ayuda a un EXPERTO a encontrar RECLUTADORES interesados en su perfil."
                : "Eres un asistente que busca usuarios con intereses/habilidades similares.";

        var recommendationType = role == Roles.Recruiter.ToString() ? "EXPERTOS" :
            role == Roles.Expert.ToString() ? "RECLUTADORES" :
            "usuarios relevantes";

        return $@"{perspective}

        Reglas estrictas:
        1. FILTRO: Selecciona exclusivamente {recommendationType}.
        2. CRITERIOS: Evalúa compatibilidad priorizando la coincidencia de intereses (prioridad alta) y la complementariedad de habilidades (prioridad media).
        3. CANTIDAD: Selecciona exactamente los 9 mejores candidatos.
        4. FORMATO: Responde ÚNICAMENTE con los IDs separados por coma, sin texto adicional, en el formato:
        id1,id2,id3,...,id9";
    }

    private static string BuildUserPrompt(User currentUser, IReadOnlyCollection<User> candidates)
    {
        var interests = FormatNames(currentUser.UserInterests?.Select(i => i.Interest?.Name));
        var skills = FormatNames(currentUser.UserSkills?.Select(s => s.Skill?.Name));

        var candidatesContext = candidates.Select(u =>
            $"{u.Id}: {FormatNames(u.UserInterests?.Select(i => i.Interest?.Name))} | {FormatNames(u.UserSkills?.Select(s => s.Skill?.Name))}");

        return $@"## TU PERFIL ##
        Intereses: {interests}
        Habilidades: {skills}

        ## CANDIDATOS DISPONIBLES ##
        {string.Join("\n", candidatesContext)}";
    }

    private static string FormatNames(IEnumerable<string?>? names) =>
        string.Join(", ", names?.Where(n => !string.IsNullOrWhiteSpace(n)) ?? []);
}
