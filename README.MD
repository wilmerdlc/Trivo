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

1. **Trust the local HTTPS development certificate** (required — the app only serves HTTPS in local dev, see [Properties/launchSettings.json](src/API/Trivo.API/Properties/launchSettings.json)):

   ```bash
   dotnet dev-certs https --trust
   ```

   Without this, `dotnet run` still starts, but browsers will reject the self-signed cert and tools like `curl` will need `-k`/`--insecure` to skip verification. On Linux, `--trust` may not be able to install the cert into the OS/browser trust store automatically — see the [official docs](https://learn.microsoft.com/aspnet/core/security/enforcing-ssl#trust-the-aspnet-core-https-development-certificate-on-linux) for distro-specific steps if that happens.

2. **Copy the env template and fill in real values:**

   ```bash
   cp .env.template .env
   ```

   Fill in the `Local (compose.yaml)` section of `.env` (DB credentials, ports, Redis/Seq passwords). You don't need the `Production` section for local development.

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
     "AiSetting": { "Model": "gpt-4o-mini", "ApiKey": "<your OpenAI API key>" }
   }
   ```

   Match the DB/Redis credentials to whatever you set in `.env` for the local containers.

5. **Run the API:**

   ```bash
   dotnet run --project src/API/Trivo.API
   ```

   This uses the `https` launch profile — the app is served at `https://localhost:7146` and applies pending EF Core migrations automatically on startup. Swagger UI opens at `/swagger`.

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
