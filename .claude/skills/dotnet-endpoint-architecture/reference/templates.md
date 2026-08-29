# Plantillas copy-paste — feature CQRS de Trivo

Reemplaza `{Feature}` (p. ej. `Interests`), `{Entity}` (p. ej. `Interest`) y `{Action}` (p. ej. `CreateInterest`)
por los nombres reales. Namespaces y rutas de carpeta son literales — respétalos.

## 1. DTO

`src/Application/Trivo.Application/DTOs/{Feature}/{Entity}DetailsDto.cs`

```csharp
namespace Trivo.Application.DTOs.{Feature};

public sealed record {Entity}DetailsDto(
    Guid {Entity}Id,
    string Name,
    Guid? CreatedBy
);
```

## 2. Command (con respuesta) — ejemplo real: `CreateInterestCommand`

`src/Application/Trivo.Application/Features/{Feature}/Commands/{Action}/{Action}Command.cs`

```csharp
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.{Feature};

namespace Trivo.Application.Features.{Feature}.Commands.{Action};

public sealed record {Action}Command(
    string Name,
    Guid? CreatedBy
) : ICommand<{Entity}DetailsDto>;
```

Command sin valor de retorno (solo éxito/fallo): `: ICommand;` y el handler implementa
`ICommandHandler<{Action}Command>` con `Task<Result>` (no `ResultT<T>`).

## 3. Validator (co-ubicado con el Command)

`.../Commands/{Action}/{Action}Validator.cs`

```csharp
using FluentValidation;

namespace Trivo.Application.Features.{Feature}.Commands.{Action};

internal sealed class {Action}Validator : AbstractValidator<{Action}Command>
{
    public {Action}Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");
    }
}
```

Se descubre solo vía `AddValidatorsFromAssembly` — no lo registres en ningún `DependencyInjection.cs`.

## 4. Handler — orquesta repos + UnitOfWork, nunca lanza excepciones de negocio

`.../Commands/{Action}/{Action}CommandHandler.cs`

```csharp
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Models;
using Trivo.Application.DTOs.{Feature};

namespace Trivo.Application.Features.{Feature}.Commands.{Action};

internal sealed class {Action}CommandHandler(
    ILogger<{Action}CommandHandler> logger,
    I{Entity}Repository repository,
    IUnitOfWork unitOfWork
) : ICommandHandler<{Action}Command, {Entity}DetailsDto>
{
    public async Task<ResultT<{Entity}DetailsDto>> Handle({Action}Command request,
        CancellationToken cancellationToken)
    {
        if (await repository.ExistsByNameAsync(request.Name, cancellationToken))
        {
            logger.LogWarning("A {{Entity}} with name '{Name}' already exists", request.Name);

            return ResultT<{Entity}DetailsDto>.Failure(
                Error.Conflict("409", "This {entity} already exists.")
            );
        }

        var entity = request.To{Entity}Entity(Guid.NewGuid());

        await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("{{Entity}} '{Name}' created with ID {Id}.", entity.Name, entity.Id);

        return ResultT<{Entity}DetailsDto>.Success(entity.To{Entity}DetailsDto());
    }
}
```

Puntos no negociables:
- Un solo `unitOfWork.SaveChangesAsync(cancellationToken)`, al final, después de todos los `repository.*Async`.
- Toda validación de negocio (existe/no existe/duplicado/permisos) retorna `Error.*` → `ResultT<T>.Failure`,
  nunca `throw`.
- `CancellationToken` siempre presente y siempre el último parámetro.

## 5. Query con paginación + cache — ejemplo real: `GetInterestsPaginationQuery`

`.../Query/{Action}/{Action}Query.cs`

```csharp
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;
using Trivo.Application.DTOs.{Feature};

namespace Trivo.Application.Features.{Feature}.Query.{Action};

public sealed record {Action}Query(
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<{Entity}Dto>>;
```

`.../Query/{Action}/{Action}QueryHandler.cs`

```csharp
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;
using Trivo.Application.DTOs.{Feature};

namespace Trivo.Application.Features.{Feature}.Query.{Action};

internal sealed class {Action}QueryHandler(
    ILogger<{Action}QueryHandler> logger,
    I{Entity}Repository repository,
    IDistributedCache cache
) : IQueryHandler<{Action}Query, PagedResult<{Entity}Dto>>
{
    public async Task<ResultT<PagedResult<{Entity}Dto>>> Handle({Action}Query request,
        CancellationToken cancellationToken)
    {
        if (request.PageNumber <= 0 || request.PageSize <= 0)
        {
            return ResultT<PagedResult<{Entity}Dto>>.Failure(
                Error.Failure("400", "Pagination parameters must be greater than zero."));
        }

        var pagedResult = await cache.GetOrCreateAsync(
            $"paginated-{feature}-{request.PageNumber}-{request.PageSize}",
            async () =>
            {
                var page = await repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
                return new PagedResult<{Entity}Dto>(
                    page.Items!.To{Entity}DtoList(), page.TotalItems, request.PageNumber, request.PageSize);
            },
            cancellationToken: cancellationToken
        );

        return ResultT<PagedResult<{Entity}Dto>>.Success(pagedResult);
    }
}
```

La clave de cache SIEMPRE incluye todos los parámetros que cambian el resultado (paginación, filtros, ids).

## 6. Mapper — extensiones estáticas, una clase por feature

`src/Application/Trivo.Application/Features/{Feature}/{Feature}Mapper.cs`

```csharp
using Trivo.Application.Features.{Feature}.Commands.{Action};
using Trivo.Domain.Models;
using Trivo.Application.DTOs.{Feature};

namespace Trivo.Application.Features.{Feature};

public static class {Feature}Mapper
{
    public static {Entity}DetailsDto To{Entity}DetailsDto(this {Entity} entity) =>
        new(
            {Entity}Id: entity.Id,
            Name: entity.Name ?? string.Empty,
            CreatedBy: entity.CreatedBy
        );

    public static IEnumerable<{Entity}Dto> To{Entity}DtoList(this IEnumerable<{Entity}> entities) =>
        entities.Select(ToShort).ToList();

    public static {Entity} To{Entity}Entity(this {Action}Command command, Guid id) => new()
    {
        Id = id,
        Name = command.Name,
        CreatedBy = command.CreatedBy,
    };

    private static {Entity}Dto ToShort(this {Entity} entity) =>
        new({Entity}Id: entity.Id, Name: entity.Name ?? string.Empty);
}
```

## 7. Repositorio — patrón MANDATORIO para entidades nuevas (extiende `IGenericRepository<T>`)

Este es el patrón que usan `User`, `Expert`, `Recruiter`, `Administrator`, `Match`, `Message`, `Chat`,
`Report`, `Notification`. Úsalo para toda entidad nueva — heredas CRUD gratis y solo escribes lo específico.

`src/Application/Trivo.Application/Interfaces/Repository/I{Entity}Repository.cs`

```csharp
using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository;

public interface I{Entity}Repository : IGenericRepository<{Entity}>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
}
```

`src/Infrastructure/Trivo.Infrastructure.Persistence/Repository/{Entity}Repository.cs`

```csharp
using Trivo.Application.Interfaces.Repository;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository;

public class {Entity}Repository(TrivoContext context) :
    GenericRepository<{Entity}>(context),
    I{Entity}Repository
{
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken) =>
        await ValidateAsync(x => x.Name == name, cancellationToken);
}
```

`Context` (protegido) y `ValidateAsync` vienen heredados de `GenericRepository<TEntity>` — no los reimplementes.
No llames `SaveChangesAsync` aquí: eso lo hace `IUnitOfWork` desde el handler.

> Patrón legado (NO replicar en repos nuevos, solo mantener si tocas uno existente): `Interest`,
> `InterestCategory`, `Skill`, `UserSkill`, `UserInterest`, `Code` extienden `IValidation<TEntity>`/
> `Validation<TEntity>` directamente y reimplementan a mano `AddAsync`/`GetPagedAsync`/etc. en vez de heredar
> de `GenericRepository<T>`. Si agregas un método a uno de esos repos, sigue su propio estilo local; si creas
> un repo nuevo desde cero, usa siempre el patrón `IGenericRepository<T>` de arriba.

## 8. DI — registrar el repo nuevo

`src/Infrastructure/Trivo.Infrastructure.Persistence/DependencyInjection.cs`, dentro de `AddRepositories`:

```csharp
services.AddScoped<I{Entity}Repository, {Entity}Repository>();
```

MediatR (handlers) y FluentValidation (validators) se auto-registran por assembly scan en
`Trivo.Application/DependencyInjection.cs` — nunca hay que tocarlo para una feature nueva.

## 9. Controller

`src/API/Trivo.API/Controllers/V1/{Feature}Controller.cs`

```csharp
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.Application.Features.{Feature}.Commands.{Action};
using Trivo.Application.Features.{Feature}.Query.{Action};
using Trivo.Application.Pagination;
using Trivo.Application.Utils;
using Trivo.Application.DTOs.{Feature};

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/{feature-kebab}")]
public class {Feature}Controller(ISender sender) : ControllerBase
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultT<{Entity}DetailsDto>> CreateAsync(
        [FromBody] {Action}Command command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("pagination")]
    public async Task<ResultT<PagedResult<{Entity}Dto>>> GetPaginatedAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new {Action}Query(pageNumber, pageSize), cancellationToken);
    }
}
```

`ISender` inyectado por constructor primario, acciones de una sola línea, tipo de retorno explícito
`Task<ResultT<T>>` / `Task<Result>` — el `ResultFilter` global se encarga de traducirlo a la respuesta HTTP.
