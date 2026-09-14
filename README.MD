# Trivo

Trivo is a matchmaking platform that connects **Experts** and **Recruiters**. Backend built with **.NET 8**, following a Clean Architecture-style layout (CQRS via MediatR, FluentValidation, EF Core + PostgreSQL, Redis cache, SignalR real-time hubs, Serilog + Seq logging, OpenAI-based recommendations).

## Project structure

```
Trivo.sln
src/
  Domain/Trivo.Domain                        # Entities, enums, domain configs
  Application/Trivo.Application              # CQRS features, DTOs, validators, interfaces
  Infrastructure/Trivo.Infrastructure.Persistence   # EF Core, repositories, Redis cache
  Infrastructure/Trivo.Infrastructure.Shared        # Email, Cloudinary, JWT, OpenAI, SignalR hubs
  API/Trivo.API                              # ASP.NET Core Web API (controllers, composition root)
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/) + Docker Compose (for PostgreSQL, Redis, Seq, pgAdmin)

## Running locally

**Quick start** (once `.env` and `appsettings.Development.json` are filled in — see steps 2 and 4 below for the first time you set this up):

```bash
dotnet dev-certs https --trust
docker compose up -d
dotnet run --project src/API/Trivo.API
```

The API is then at `https://localhost:7146`, with the API reference UI at `https://localhost:7146/scalar`.

Full steps, for the first time you set this up:

1. **Trust the local HTTPS development certificate** (required — the app only serves HTTPS in local dev, see [Properties/launchSettings.json](src/API/Trivo.API/Properties/launchSettings.json)):

   ```bash
   dotnet dev-certs https --trust
   ```

   Without this, `dotnet run` still starts, but browsers will reject the self-signed cert and tools like `curl` will need `-k`/`--insecure` to skip verification. On Linux, `--trust` may not be able to install the cert into the OS/browser trust store automatically — see the [official docs](https://learn.microsoft.com/aspnet/core/security/enforcing-ssl#trust-the-aspnet-core-https-development-certificate-on-linux) for distro-specific steps if that happens.

2. **Copy the env template and fill in real values:**

   ```bash
   cp .env.template .env
   ```

   Fill in the `Local (compose.yaml)` section of `.env` (DB credentials, ports, Redis/Seq passwords). You don't need the `Production` section for local development. If you'd rather not have the unused `Production` variables in your `.env` at all, use `.env.development.template` instead — it only has the `Local` ones.

3. **Start the infrastructure containers** (PostgreSQL, pgAdmin, Seq, Redis):

   ```bash
   docker compose up -d
   ```

4. **Create your local secrets file.** `src/API/Trivo.API/appsettings.Development.json` is gitignored — create it yourself with the same shape as `appsettings.json`, filled with real local values instead of `CHANGE_ME` placeholders:

   ```json
   {
     "ConnectionStrings": {
       "TrivoContext": "Host=localhost;Port=5432;Database=<POSTGRES_DB>;Username=<POSTGRES_USER>;Password=<POSTGRES_PASSWORD>",
       "Redis": "localhost:6379,password=<REDIS_PASSWORD>"
     },
     "JwtSetting": { "Key": "...", "Issuer": "...", "Audience": "...", "DurationInMinutes": 30 },
     "EmailSetting": { "EmailFrom": "...", "SmtpHost": "...", "SmtpPort": 587, "SmtpUser": "...", "SmtpPassword": "...", "DisplayName": "..." },
     "CloudinarySetting": { "CloudinaryUrl": "..." },
     "AiSetting": { "Provider": "OpenAI", "EmbeddingModel": "text-embedding-3-small", "ApiKey": "<your OpenAI API key>" }
   }
   ```

   Match the DB/Redis credentials to whatever you set in `.env` for the local containers.

5. **Run the API:**

   ```bash
   dotnet run --project src/API/Trivo.API
   ```

   This uses the `https` launch profile — the app is served at `https://localhost:7146` and applies pending EF Core migrations automatically on startup. The API reference UI (Scalar, browsing the same OpenAPI spec Swagger generates) opens at `/scalar`.

   Seq's web UI is available at `http://localhost:<SEQ_PORT_UI>` (whatever you set in `.env`) for browsing structured logs.

## Health checks

`GET /health` reports the status of the API's critical dependencies (PostgreSQL and Redis). No authentication required.

```bash
curl -k https://localhost:7146/health
```

Example response when everything is healthy:

```json
{
  "status": "Healthy",
  "totalDurationMs": 12.34,
  "checks": [
    { "name": "postgresql", "status": "Healthy", "description": null, "durationMs": 8.1 },
    { "name": "redis", "status": "Healthy", "description": null, "durationMs": 3.2 }
  ]
}
```

- HTTP **200** when every check is `Healthy`.
- HTTP **503** when any check is `Degraded` or `Unhealthy` — the response body includes a `description` explaining what failed (e.g. connection refused).
- Routine requests to `/health` are logged at `Verbose` (filtered out of Seq by default); failures are logged at `Error` so they still show up.
- In `docker-compose`/`compose.prod.yaml`, this same endpoint drives the `api` service's container healthcheck, and other services can `depends_on: api: condition: service_healthy`.

## API responses — Result Pattern

Handlers never build the HTTP response — they return `Trivo.Application.Utils.Result` / `ResultT<T>`, and
`Trivo.API.Filters.ResultFilter` (a global `IAsyncActionFilter` registered in `Program.cs`) translates that
into the actual status code and body. Controllers stay a one-line `return await sender.Send(...)`, with an
explicit `Task<Result>` / `Task<ResultT<T>>` return type — never `IActionResult`.

See [`src/API/Trivo.API/Trivo.API.http`](src/API/Trivo.API/Trivo.API.http) for runnable examples of every
case below.

### Success

| Handler returns | Response |
|---|---|
| `ResultT<T>.Success(value)` | **200 OK** with `value` as the body |
| `ResultT<T>.Created(value)` | **201 Created** with `value` as the body |
| `ResultT<T>.Accepted(value)` | **202 Accepted** with `value` as the body |
| `Result.Success()` (no value) | **204 No Content** |
| `Result.Accepted()` (no value) | **202 Accepted**, empty body |

**Every `Create*CommandHandler` uses `ResultT<T>.Success(dto)` → 200 OK, not 201.** This is deliberate: the
frontend consumes the created resource's DTO straight from the response body, and every other endpoint
(reads, updates) already returns 200 — keeping creates on 200 too means the client handles one predictable
status instead of branching on 201 for some POSTs and not others. `Created`/`Accepted` exist in the library
for future async/job-style endpoints, not for the current create flows.

`ResultFilter` unwraps `ResultT<T>` and writes `T` straight to the body — there's no `{ isSuccess, value }`
envelope, the body **is** the DTO. Example: `POST /api/v1/ability` (`CreateSkillCommandHandler`, returns
`ResultT<SkillDto>.Success(...)`):

```json
// 200 OK
{
  "skillId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Advanced C#",
  "registeredAt": "2026-09-12T14:30:00Z"
}
```

Paginated queries (`ResultT<PagedResult<T>>.Success(...)`) are the same rule — the body is the
`PagedResult<T>` itself, not the raw list. Example: `GET /api/v1/ability/pagination?pageNumber=1&pageSize=10`
(`GetSkillsPaginationQueryHandler`):

```json
// 200 OK
{
  "items": [
    { "skillId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", "name": "Advanced C#", "registeredAt": "2026-09-12T14:30:00Z" },
    { "skillId": "9c858901-8a57-4791-81fe-4c455b099bc9", "name": "SQL", "registeredAt": "2026-08-01T09:15:00Z" }
  ],
  "totalItems": 2,
  "currentPage": 1,
  "totalPages": 1
}
```

`totalPages` is computed server-side (`Math.Ceiling(totalItems / pageSize)`) — clients don't need to derive
it from `totalItems`/`pageSize` themselves. Every `PagedResult<T>` in the API (`Trivo.Application.Pagination`)
has this exact shape, regardless of what `T` is.

### Errors

Every business error (`Result.Failure(error)` / `ResultT<T>.Failure(error)`) becomes an
[RFC 9457](https://www.rfc-editor.org/rfc/rfc9457) `ProblemDetails` body, with the app's own error code in
`errorCode`:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Not Found",
  "status": 404,
  "detail": "The specified user was not found.",
  "errorCode": "404"
}
```

`Error.ErrorType` decides the status code (single source of truth:
`src/API/Trivo.API/Filters/ProblemDetailsMapper.cs`):

| `ErrorType` | Status |
|---|---|
| `Validation` | 400 |
| `Unauthorized` | 401 |
| `Forbidden` | 403 |
| `NotFound` | 404 |
| `Conflict` | 409 |
| `TooManyRequests` | 429 |
| `ExternalService` | 502 |
| `Unavailable` | 503 |
| `Timeout` | 504 |
| `Unexpected` / anything else | 500 |
| `Custom` | whatever `Error.StatusCode` says — escape hatch for status codes no `ErrorType` covers (402, 422, etc.) |

### Three response shapes still coexist (known gap)

Business errors above are the standardized path, but two other error sources in the API don't go through
`ResultFilter` and look different on the wire:

1. **FluentValidation failures** — a malformed request caught by `ValidationBehavior` before the handler
   ever runs — are caught by `ExceptionHandlingMiddleware`. Still `ProblemDetails`, but with a per-field
   `errors` dictionary instead of `errorCode`:
   ```json
   {
     "type": "ValidationFailure",
     "title": "Validation errors",
     "status": 400,
     "errors": { "Name": ["The skill name is required."] }
   }
   ```
2. **JWT authentication/authorization failures** (missing/expired/invalid token, insufficient role) are
   handled directly by `JwtBearerEvents` in `Trivo.Infrastructure.Shared/DependencyInjection.cs` and don't
   use `ProblemDetails` at all — they serialize a `JwtResponse(bool HasError, string? Error)` with
   `Newtonsoft.Json`, which defaults to **PascalCase** (the rest of the API uses `System.Text.Json`'s
   camelCase):
   ```json
   { "HasError": true, "Error": "An unexpected authentication error occurred" }
   ```

Unifying these two into the same `ProblemDetails`/`errorCode` shape is a natural next step, not yet done.

## Building the Docker image

The `Dockerfile` builds and publishes `Trivo.API` in a multi-stage build. Build it from the **repository root** (the `COPY` paths inside the Dockerfile are relative to it):

```bash
docker build -t trivo-backend:local -f Dockerfile .
```

This step doesn't need any environment variables or secrets — those are only required at container runtime (see below). To publish it for deployment, tag and push it to the registry referenced by `API_IMAGE` in your production `.env`:

```bash
docker build -t wdelacruzdev/trivo-backend:<tag> -f Dockerfile .
docker push wdelacruzdev/trivo-backend:<tag>
```

## Running the full stack in "production" mode

`compose.prod.yaml` runs the same infrastructure as `compose.yaml` plus the dockerized API itself, wiring all secrets in as environment variables at runtime (never baked into the image — see `Dockerfile` comments for why).

1. Fill in the full `.env` (both the `Local` and `Production` sections from `.env.template`), including `API_IMAGE` pointing at an image you've built/pushed (or built locally per the step above).
2. Start everything:

   ```bash
   docker compose -f compose.prod.yaml up -d
   ```

3. The API is reachable at `http://localhost:<API_PORT>` (HTTP only — there's no TLS certificate configured on the container yet; TLS termination is expected to happen at a reverse proxy/load balancer once one exists, see `HSTS_IMPLEMENTATION_PLAN.md`).
4. Check `docker compose -f compose.prod.yaml ps` — the `api` service won't report `healthy` until `/health` returns 200, which itself won't happen until Postgres and Redis are reachable.

## Logging

Serilog writes to the console and to Seq (`compose.yaml`/`compose.prod.yaml` both run a `seq` container). Locally, Seq's UI is at `http://localhost:<SEQ_PORT_UI>`; in production it resolves internally via the `seq` service name (see `appsettings.Production.json`).

## Working with this repo in Claude Code (optional)

None of this is required to build, run, or contribute to Trivo — it only matters if you use [Claude Code](https://claude.com/claude-code) against this repo, backend or frontend side.

- **`.claude/skills/`** — committed, project-specific skills (CQRS/Result/Repository conventions, EF Core patterns, nullable reference types, etc.). They're picked up automatically by Claude Code from this repo; no install step needed.
- **`CLAUDE.md`** — repo-level instructions Claude Code always loads, including the rules for using `graphify` (see below).
- **`graphify-out/`** — a generated knowledge graph of the codebase (god nodes, community structure, cross-file relationships: `graph.json`, `GRAPH_REPORT.md`, `manifest.json`, `graph.html`). It's committed so the graph is available without anyone having to generate it first; `graphify-out/cache/` (pure local cache, regenerable) is gitignored.
- **`graphify`** itself is a separate CLI tool, not a dependency of this repo — it's only needed if you want Claude to query the graph (`graphify query "<question>"`, `graphify path "<A>" "<B>"`, `graphify explain "<concept>"`) instead of grepping raw files. To set it up locally:

  ```bash
  uv tool install graphifyy   # or: pipx install graphifyy
  graphify claude install     # wires the CLAUDE.md hook + PreToolUse guard for your machine
  ```

  `graphify claude install` writes machine-specific hooks to `.claude/settings.local.json`, which is gitignored — each developer sets this up independently, it's never shared via git.
- After changing code, run `graphify update .` to refresh the graph (AST-only, no API cost) so it stays accurate for the next session.
