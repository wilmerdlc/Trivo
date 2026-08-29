---
name: dotnet-endpoint-architecture
description: Mandato de arquitectura .NET para Trivo. Úsalo SIEMPRE que vayas a crear o modificar un endpoint, un Command, una Query, un handler, un validator, un repositorio, un mapper o un método de UnitOfWork en Trivo.API / Trivo.Application / Trivo.Infrastructure.Persistence. También úsalo al revisar código nuevo para verificar que sigue la convención CQRS + Result + Repository/UnitOfWork del proyecto. Dispara con: "nuevo endpoint", "nuevo command", "nueva query", "crear feature", "repositorio", "controller", CRUD de una entidad nueva.
---

# Arquitectura CQRS de Trivo — mandato para nuevas features

Trivo usa **Clean Architecture** (Domain → Application → Infrastructure → API) con **CQRS vía MediatR**,
patrón **Result/ResultT** para errores de negocio (sin excepciones), **Repository** por entidad y un
**Unit of Work delgado** (el propio `DbContext` implementa `IUnitOfWork`). Toda feature nueva DEBE replicar
exactamente esta estructura — no inventes variantes (no `IActionResult`, no `try/catch` de negocio en
handlers, no `SaveChanges` dentro de un repositorio).

Este documento es el mandato corto y accionable. El detalle línea por línea con plantillas copy-paste
está en `reference/templates.md` — cárgalo cuando vayas a escribir el código real de una feature nueva.

## Flujo de referencia rápido

```
HTTP Request
  → Controller (thin, [ApiController], inyecta ISender)
    → sender.Send(Command/Query)
      → ValidationBehavior<TRequest,TResponse>  (FluentValidation, pipeline de MediatR)
        → CommandHandler / QueryHandler          (orquesta repos + IUnitOfWork, retorna Result/ResultT)
          → I{Entity}Repository                  (EF Core sobre TrivoContext, sin SaveChanges)
          → IUnitOfWork.SaveChangesAsync()        (== TrivoContext.SaveChangesAsync, una sola vez por Command)
  ← ResultFilter (Program.cs, global)             (desenvuelve Result → 200/4xx automáticamente)
  ← ExceptionHandlingMiddleware                    (ValidationException → 400 ProblemDetails; resto → 500)
```

## Checklist para agregar una feature nueva ("{Feature}" / "{Action}" / "{Entity}")

1. **DTO(s)** en `Trivo.Application/DTOs/{Feature}/{Nombre}Dto.cs` — `sealed record`, nombres cortos
   (`{Entity}Dto`, `{Entity}DetailsDto`, `{Entity}WithIdDto`, según lo que exponga el endpoint).
2. **Command o Query**:
   - Command con retorno: `Trivo.Application/Features/{Feature}/Commands/{Action}/{Action}Command.cs`
     → `sealed record {Action}Command(...) : ICommand<TResponse>;`
   - Command sin retorno (solo éxito/fallo): `: ICommand;`
   - Query: `Trivo.Application/Features/{Feature}/Query/{Action}/{Action}Query.cs`
     → `sealed record {Action}Query(...) : IQuery<TResponse>;`
   - Nota la carpeta **singular `Query`** (no `Queries`) — así está en todo el proyecto.
3. **Validator** (FluentValidation) en la misma carpeta que el Command/Query:
   `internal sealed class {Action}Validator : AbstractValidator<{Action}Command>`.
   No se registra a mano: `AddValidatorsFromAssembly` en `Trivo.Application/DependencyInjection.cs`
   lo descubre solo. El `ValidationBehavior` lanza `ValidationException` si falla — no la captures en el handler.
4. **Handler** en la misma carpeta:
   `internal sealed class {Action}CommandHandler(ILogger<...> logger, I{Entity}Repository repo, IUnitOfWork unitOfWork, ...)`
   `: ICommandHandler<{Action}Command, TResponse>` (o `IQueryHandler<...>` para queries).
   - Reglas de negocio (existe / no existe / duplicado) → `return ResultT<T>.Failure(Error.NotFound/Conflict/Unauthorized/Failure(...))`.
     **Nunca lances excepciones para errores esperados.**
   - Un solo `await unitOfWork.SaveChangesAsync(cancellationToken)` al final, después de todos los
     `repo.AddAsync/UpdateAsync/DeleteAsync`. El repositorio NUNCA llama a SaveChanges.
   - `CancellationToken` es siempre el último parámetro, en cada método async.
   - Log estructurado: `LogWarning` en fallos de negocio, `LogInformation` en éxito con los IDs relevantes.
   - Queries de listado/paginación costosas: envolver en `IDistributedCache.GetOrCreateAsync("clave-con-todos-los-parametros", ...)`.
5. **Mapper**: extensión estática en `Trivo.Application/Features/{Feature}/{Feature}Mapper.cs`
   (`To{Entity}Entity`, `To{X}Dto`, `To{X}DtoList`). El handler nunca construye el DTO a mano inline.
6. **Repositorio**:
   - Interfaz en `Trivo.Application/Interfaces/Repository/[Account/]I{Entity}Repository.cs`.
   - Implementación en `Trivo.Infrastructure.Persistence/Repository/[Account/]{Entity}Repository.cs`.
   - **Mandato para entidades nuevas**: extiende `IGenericRepository<TEntity>` /
     `GenericRepository<TEntity>(context)` (ver `Base/GenericRepository.cs`) para heredar
     `GetByIdAsync/GetPagedAsync/CreateAsync/UpdateAsync/DeleteAsync/ValidateAsync` gratis y solo agregar
     los métodos específicos de la entidad. Este es el patrón que usan `User`, `Expert`, `Recruiter`,
     `Administrator`, `Match`, `Message`, `Chat`, `Report`, `Notification`.
     — Existe un segundo patrón más antiguo (`Interest`, `InterestCategory`, `Skill`, `UserSkill`,
     `UserInterest`, `Code`) que extiende solo `IValidation<TEntity>`/`Validation<TEntity>` y reimplementa
     el CRUD a mano. Es legado: **no lo repliques en repos nuevos**, solo respétalo si extiendes uno de esos.
   - Lecturas: `AsNoTracking()` siempre. Multi-`Include`: agrega `.AsSplitQuery()`. Búsqueda case-insensitive
     en Postgres: `EF.Functions.ILike(campo, $"%{texto}%")`.
7. **DI**: registra el repo nuevo en `AddRepositories` dentro de
   `Trivo.Infrastructure.Persistence/DependencyInjection.cs` con `services.AddScoped<I{Entity}Repository, {Entity}Repository>();`
   (usa `Scoped`, no `Transient`, para nuevas entradas — coherente con el ciclo de vida del `DbContext`;
   el `Transient` que verás en varias entradas existentes es deuda técnica, no el estándar a seguir).
   MediatR ya escanea el assembly completo (`AddApplicationLayer`), no hay que registrar handlers a mano.
8. **Controller** en `Trivo.API/Controllers/V1/{Feature}Controller.cs`:
   - `[ApiController] [ApiVersion("1.0")] [Route("api/v{version:apiVersion}/{recurso-en-plural-kebab}")]`
   - Constructor primario `({Feature}Controller(ISender sender) : ControllerBase)`.
   - Acción = una línea: `return await sender.Send(command o new {Action}Query(...), cancellationToken);`
   - Tipo de retorno **explícito** `Task<ResultT<TDto>>` o `Task<Result>` — nunca `IActionResult`/`ActionResult<T>`.
     El `ResultFilter` global (registrado en `Program.cs`) desenvuelve `Result`/`ResultT<T>` a 200 OK o al
     status code mapeado por `ErrorType` (`NotFound→404`, `Conflict→409`, `Unauthorized→401`, resto→400).
   - `[Authorize]` **por acción**, no a nivel de clase (mismo criterio que el resto de controllers: lectura
     pública si aplica, escritura protegida).
   - `[ProducesResponseType(StatusCodes.Status200OK)]` + `[ProducesResponseType(StatusCodes.Status400BadRequest)]`
     como mínimo.

## Reglas duras (no negociables)

- **Nunca** manejes `try/catch` de reglas de negocio en un handler — todo error esperado es un `Error` →
  `Result.Failure`/`ResultT<T>.Failure`. La única excepción que se deja propagar intencionalmente es
  `FluentValidation.ValidationException`, capturada centralmente por `ExceptionHandlingMiddleware`.
- **Nunca** llames `SaveChangesAsync` desde un repositorio — es responsabilidad exclusiva de `IUnitOfWork`,
  invocado una vez por Command desde el handler.
- **Nunca** devuelvas `IActionResult`/`ActionResult<T>` en un controller nuevo — rompe el `ResultFilter`.
- **Nunca** pongas lógica de negocio en el controller — es un traductor HTTP↔MediatR de una línea.
- **Nunca** actualices/leas entidades sin `AsNoTracking()` en queries de solo lectura.
- El código de error (`"404"`, `"409"`, etc.) es solo un identificador textual — el status HTTP real lo
  decide `ErrorType` en `ResultFilter.MapToStatusCode`, no el string.

Para el boilerplate completo copy-paste (Command, Handler, Validator, Mapper, Repository interfaz +
implementación, Controller) usa `reference/templates.md`.
