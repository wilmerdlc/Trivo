# Plan de implementación — Embeddings vía API externa para emparejamiento por afinidad

Decisión tomada: generar los embeddings usando una **API externa** (OpenAI `text-embedding-3-small` como primaria; Google `text-embedding-004`/Gemini como alternativa intercambiable), no un modelo self-hosted. La búsqueda de similitud sigue siendo responsabilidad de Postgres + pgvector, sin cambios respecto al diseño original.

Este documento reemplaza el flujo actual de `GetUserRecommendationsQueryHandler` (prompt-stuffing a un LLM de chat) por embeddings + búsqueda vectorial.

## 0. Decisión de proveedor y por qué la interfaz debe ser agnóstica

- Proveedor primario: **OpenAI** — ya está integrado en el repo (`Trivo.Infrastructure.Shared` referencia el SDK `OpenAI 2.12.0`, y `AiSetting`/`AI_API_KEY` ya existen en `appsettings.json` y `.env.template`). Usar `text-embedding-3-small` (1536 dimensiones, $0.02/1M tokens).
- Proveedor alternativo: **Google** (`text-embedding-004` vía Gemini API) — se deja documentado pero no se implementa en esta fase. Si más adelante se cambia, solo debe afectar a `Trivo.Infrastructure.Shared`, nunca a `Trivo.Application`.
- Por eso el primer paso es siempre definir `IEmbeddingService` en `Trivo.Application.Interfaces.Services`. El resto del sistema (Command handlers, repositorios, columna `vector` en Postgres) no debe saber qué proveedor hay detrás.

## 1. Infraestructura de base de datos (bloqueante, va primero)

El Postgres actual (`postgres:17-alpine` en `compose.yaml` y `compose.prod.yaml`) **no trae la extensión `pgvector`**. Sin esto nada más funciona.

- [ ] Cambiar la imagen de Postgres en `compose.yaml` y `compose.prod.yaml` de `postgres:17-alpine` a `pgvector/pgvector:pg17`.
- [ ] Agregar a la primera migración de EF Core (o a un script previo) `CREATE EXTENSION IF NOT EXISTS vector;`.
- [ ] Añadir paquetes NuGet en `Trivo.Infrastructure.Persistence`: `Pgvector` y `Pgvector.EntityFrameworkCore`.
- [ ] En `AddDbContext` (`Trivo.Infrastructure.Persistence/DependencyInjection.cs`), agregar `.UseVector()` a la configuración de `UseNpgsql(...)`.

## 2. Dominio (`Trivo.Domain`)

- [ ] Agregar a `User.cs` (`src/Domain/Trivo.Domain/Models/User.cs`) la propiedad `public Vector? ProfileEmbedding { get; set; }` (tipo `Pgvector.Vector`).
- [ ] Agregar `AiSetting` (`src/Domain/Trivo.Domain/Configurations/AiSetting.cs`) un campo `EmbeddingModel` (ej. `text-embedding-3-small`), separado de `Model` (que sigue siendo el de chat, si se conserva algún otro uso de `IAiCompletionService`; ver sección 7).

## 3. Application (`Trivo.Application`)

- [ ] Nueva interfaz `IEmbeddingService` en `Interfaces/Services/`:
  ```csharp
  public interface IEmbeddingService
  {
      Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken = default);
  }
  ```
- [ ] Nuevo helper estático `UserProfileTextBuilder` (mismo espíritu que `UserRecommendationPromptBuilder`, pero produce el bloque de texto plano del perfil: bio + intereses + skills + disponibilidad) — usado tanto al generar el embedding como, opcionalmente, para debug/logging.
- [ ] Nuevo método en `IUserRepository`: `Task<IReadOnlyList<User>> GetSimilarUsersAsync(Guid userId, Vector embedding, Roles targetRole, int topN, CancellationToken ct)`. Este es el que reemplaza la lógica de `GetSimilarUsers` (fuerza bruta por conteo) y el prompt al LLM.
- [ ] Reescribir `GetUserRecommendationsQueryHandler`:
  - Quitar la dependencia de `IAiCompletionService`, `UserRecommendationPromptBuilder`, el parseo por regex de GUIDs y el fallback en cascada.
  - Leer `currentUser.ProfileEmbedding` (si es `null`, generar uno on-demand como fallback de datos legados — ver sección 6) y llamar a `GetSimilarUsersAsync`.
  - Ajustar/eliminar la key de caché `ai-recommendation-response-{userId}` — con embeddings guardados, la búsqueda es lo bastante barata como para no necesitar cachear la respuesta completa; si se cachea algo, cachear el resultado de la query vectorial con una key que invalide al cambiar el pool de candidatos (o quitar el cache y medir).

## 4. Infrastructure.Shared — implementación del proveedor

- [ ] Nueva clase `OpenAiEmbeddingService : IEmbeddingService` en `Trivo.Infrastructure.Shared/Services/`, usando `EmbeddingClient` del mismo SDK `OpenAI` ya referenciado (análogo a como `OpenAiCompletionService` usa `ChatClient`).
- [ ] Registrar en `DependencyInjection.cs` → `AddAiService`: `services.AddScoped<IEmbeddingService, OpenAiEmbeddingService>();`.
- [ ] Manejo de errores: si la API externa falla o hace timeout, el Command handler que dispara la generación (sección 5) debe decidir si reintenta, encola, o deja el perfil sin embedding para regenerar después — no debe tumbar la operación de "guardar perfil".

## 5. Infrastructure.Persistence — columna, mapping e índice

- [ ] `UserConfig.cs` (`Configurations/UserConfig.cs`): mapear `ProfileEmbedding` a columna `vector(1536)`.
- [ ] Migración EF Core: agregar la columna y, en el mismo `Up()`, el índice HNSW vía SQL crudo:
  ```sql
  CREATE INDEX IF NOT EXISTS ix_users_profile_embedding_hnsw
  ON "Users" USING hnsw ("ProfileEmbedding" vector_cosine_ops);
  ```
- [ ] Implementar `GetSimilarUsersAsync` en `UserRepository` usando el operador `<=>` de pgvector (vía `EF.Functions` de `Pgvector.EntityFrameworkCore` o SQL crudo con `FromSqlInterpolated`), con `AsNoTracking()` (política ya establecida en el proyecto para queries de solo lectura) y filtros de rol/exclusión del propio usuario aplicados en la misma consulta.

## 6. Cuándo se genera/regenera el embedding

Los puntos donde hoy se modifican datos que afectan el "significado" del perfil (bio, intereses, skills) están dispersos: `CreateUserCommandHandler`, `UpdateBiographyCommandHandler`, y los comandos que asignan skills/intereses al usuario. Enumerar exhaustivamente todos esos handlers durante la implementación.

- [ ] Definir un evento de dominio `UserProfileChangedEvent(UserId)` disparado por cualquiera de esos handlers, en vez de llamar a `IEmbeddingService` directamente desde cada uno (evita duplicar la llamada en N sitios y evita que cada handler dependa de infraestructura de IA).
- [ ] Un único `UserProfileChangedEventHandler` (o job) escucha el evento, arma el texto con `UserProfileTextBuilder`, llama a `IEmbeddingService.GetEmbeddingAsync`, y guarda el vector.
- [ ] Decidir síncrono vs. asíncrono: dado que es una API externa (latencia de red variable), NO generar el embedding en el mismo request/transacción que guarda el perfil si eso puede alargar perceptiblemente la respuesta al usuario. Alternativas, de más simple a más robusta:
  1. Llamada síncrona pero fuera de la transacción principal (perfil se guarda igual aunque falle el embedding).
  2. Fire-and-forget con `IHostedService`/background queue (hay `IDistributedCache`/Redis ya en el proyecto; se puede usar una cola simple).
  - Para el alcance de una tesis, la opción 1 es suficiente y mucho menos trabajo; documentarlo como decisión consciente.

## 7. Migración de datos existentes (backfill)

- [ ] Script/comando one-off (ej. un endpoint admin temporal o una consola) que recorra los usuarios sin `ProfileEmbedding` y lo genere en batch, respetando rate limits de la API externa.
- [ ] Sin esto, los usuarios creados antes del cambio nunca aparecerán en resultados de similitud.

## 8. Limpieza del código actual

- [ ] Confirmar que `IAiCompletionService`/`OpenAiCompletionService` no se usan en ningún otro lado (hoy solo los usa `GetUserRecommendationsQueryHandler`). Si no queda otro consumidor, eliminarlos junto con `UserRecommendationPromptBuilder` en vez de dejarlos muertos en el repo.
- [ ] Quitar el regex `GuidPattern()` y el fallback en cascada (`GetSimilarUsers` por conteo de intereses/skills) del handler — ya no aplica con búsqueda vectorial.

## 9. Configuración y secretos

- [ ] `appsettings.json` / `appsettings.Development.json`: agregar `AiSetting:EmbeddingModel`.
- [ ] `.env.template`: agregar `AI_EMBEDDING_MODEL=xxx` junto a `AI_MODEL`/`AI_API_KEY` ya existentes (reusar la misma `API_KEY`, mismo proveedor).
- [ ] `appsettings.Production.json` / variables de `compose.prod.yaml`: igual.

## 10. Pruebas

- [ ] Unit test de `UserProfileTextBuilder` (determinístico, sin red).
- [ ] Unit test del handler de recomendaciones con `IEmbeddingService` y `IUserRepository` mockeados (verificar que ya no depende de parseo de texto libre).
- [ ] Test de integración (con Postgres real, `pgvector/pgvector:pg17` en contenedor de test) que verifique que `GetSimilarUsersAsync` devuelve resultados ordenados por distancia coseno.

## Orden recomendado de ejecución

1. Infra de Postgres (sección 1) — sin esto no se puede probar nada más.
2. Dominio + Application (secciones 2-3) — definir contratos antes de implementar.
3. Infrastructure.Shared (sección 4) — implementación concreta con OpenAI.
4. Persistence + migración (sección 5).
5. Trigger de generación (sección 6) y reemplazo del query handler (parte de sección 3, cerrar aquí).
6. Backfill (sección 7).
7. Limpieza (sección 8) + config (sección 9) + tests (sección 10).
