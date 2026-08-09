# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Trivo.sln ./
COPY src/Domain/Trivo.Domain/Trivo.Domain.csproj ./src/Domain/Trivo.Domain/
COPY src/Application/Trivo.Application/Trivo.Application.csproj ./src/Application/Trivo.Application/
COPY src/Infrastructure/Trivo.Infrastructure.Shared/Trivo.Infrastructure.Shared.csproj ./src/Infrastructure/Trivo.Infrastructure.Shared/
COPY src/Infrastructure/Trivo.Infrastructure.Persistence/Trivo.Infrastructure.Persistence.csproj ./src/Infrastructure/Trivo.Infrastructure.Persistence/
COPY src/API/Trivo.API/Trivo.API.csproj ./src/API/Trivo.API/

RUN dotnet restore Trivo.sln

# Copy the rest of the source code
COPY . .

WORKDIR /app/src/API/Trivo.API

RUN dotnet publish Trivo.API.csproj -c Release -o /app/publish --no-restore -p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/* \
    && chown app:app /app

# dotnet publish already included appsettings.json and appsettings.Production.json
# inside /app/publish automatically (default SDK behavior for any
# appsettings*.json present in the project folder), so this single copy
# already brings them along. Neither one has real secrets.
#
# IMPORTANT: for that same reason, appsettings.Development.json must stay
# excluded in .dockerignore — if it weren't, this same automatic mechanism
# would bake it into the image with the development secrets, without any
# line in this Dockerfile ever mentioning it explicitly.
COPY --from=build --chown=app:app /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5026

EXPOSE 5026

USER app

# Redundant with the healthcheck in docker-compose.prod.yml, but keeps the
# image functional on its own (plain docker run, Swarm, etc. honor it too).
HEALTHCHECK --interval=10s --timeout=5s --retries=5 --start-period=20s \
    CMD curl -f http://localhost:5026/health || exit 1

ENTRYPOINT ["dotnet", "Trivo.API.dll"]
