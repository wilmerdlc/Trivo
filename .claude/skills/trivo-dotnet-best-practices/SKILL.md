---
name: trivo-dotnet-best-practices
description: Checklist central de buenas prácticas .NET 8 / C# 12 / EF Core para Trivo. Úsalo SIEMPRE antes de escribir, modificar, corregir o revisar CUALQUIER archivo .cs del repo — features nuevas, bugfixes de una línea, queries/repositorios EF Core, handlers, mappers, DTOs, controllers, DI, servicios. Indica qué skill detallado cargar según el tipo de cambio (efcore-patterns, database-performance, modern-csharp-coding-standards, csharp-nullable-reference-types, dependency-injection-patterns, api-design, dotnet-endpoint-architecture, dotnet-slopwatch). Dispara con: "fix", "bug", "issue", "endpoint", "query", "repositorio", "Include", "handler", "refactor", "revisar código", ".cs".
---

# Buenas prácticas .NET para Trivo — checklist central

Stack: **.NET 8, C# 12, NRT habilitado, EF Core + PostgreSQL (Npgsql), MediatR CQRS, FluentValidation,
Result pattern.** No uses features de C# 13/14 (p. ej. `field` keyword) — no compilan aquí.

Este skill es el punto de entrada corto. Para el detalle, carga el skill específico de la tabla.

## 1. ¿Qué skill detallado cargar?

| Si el cambio toca… | Carga además |
|---|---|
| Endpoint / Command / Query / Handler / Validator / Mapper / Repo nuevo | `dotnet-endpoint-architecture` (+ su `reference/templates.md`) |
| Cualquier query LINQ, `Include`, repositorio, migración, `DbContext` | `efcore-patterns` |
| Listados, paginación, búsqueda, endpoints lentos, N+1 | `database-performance` |
| Records, pattern matching, async, colecciones, value objects | `modern-csharp-coding-standards` |
| `?`, `!`, nulls en DTOs/entidades, warnings CS86xx | `csharp-nullable-reference-types` |
| `DependencyInjection.cs`, lifetimes, `Program.cs` | `dependency-injection-patterns` |
| Cambiar la forma de un DTO/contrato ya consumido por el front | `api-design` |
| Al terminar cualquier cambio no trivial | `dotnet-slopwatch` (si está instalado) |

## 2. EF Core — reglas que más bugs causan en Trivo

1. **Carga explícitamente toda navegación que el mapper lea.** Antes de cerrar un repo, abre el
   `*Mapper.cs` que consume la entidad y verifica que cada `entidad.Nav?.Prop` tenga su
   `Include`/`ThenInclude`. Un `Include` faltante no falla: devuelve `null` silencioso en el JSON.
   - Con `AsNoTracking()` **no hay identity resolution**: que `Report.ReportedUser` esté cargado NO
     llena `Report.Sanction.User`. Cada ruta de navegación se incluye por separado.
   - Ejemplo real (issue #45): `.Include(r => r.Sanction)` sin `.ThenInclude(s => s!.Admin)` →
     `sanction.adminUsername: null`.
2. **Lecturas**: `AsNoTracking()` siempre. Para listados prefiere proyección `Select(...)` a DTO
   sobre `Include` masivo — trae solo columnas necesarias.
3. **Varios `Include` de colecciones** → `.AsSplitQuery()` (evita explosión cartesiana).
4. **Nunca** materialices antes de filtrar (`ToListAsync()` y luego `Where`), ni hagas joins en memoria,
   ni queries dentro de un `foreach` (N+1). Usa `AnyAsync` para existencia, no `CountAsync() > 0`.
5. **Paginación** siempre con `OrderBy` determinista antes de `Skip/Take`.
6. **Búsqueda** case/accent-insensitive: `EF.Functions.ILike(...)` (+ `unaccent` ya habilitado).
7. **Escrituras**: el repo agrega/modifica; `IUnitOfWork.SaveChangesAsync` una sola vez en el handler.
   Nunca `SaveChanges` en un repositorio.
8. **Migraciones**: generadas con `dotnet ef`, nunca editadas a mano salvo SQL crudo justificado
   (como la de `unaccent`). Revisa el `Up/Down` generado antes de aceptarlo.
9. **Fechas**: `DateTime.UtcNow` (Npgsql `timestamptz` exige UTC).

## 3. C# / arquitectura

- `async` de punta a punta; `CancellationToken` último parámetro **y propagado** a cada llamada EF/IO.
  Nunca `.Result`, `.Wait()`, `async void`.
- DTOs: `sealed record`. Clases que no se heredan: `sealed`. Handlers/validators: `internal sealed`.
- Errores esperados → `Result.Failure(Error.X(...))`; excepciones solo para lo excepcional.
  Acceso a recurso ajeno → `Error.Forbidden` (403), no `Unauthorized` (401 = no autenticado).
- NRT: no uses `!` para silenciar el compilador salvo en lambdas de `Include/ThenInclude` sobre
  navegaciones nullable (patrón aceptado del repo). Valida en el borde, no propagues nulls.
- Logging estructurado: `logger.LogInformation("Report '{ReportId}' ...", id)` — nunca interpolación `$""`.
- Lifetimes: repos/servicios con `DbContext` → `Scoped`.
- Sin código muerto, sin `catch {}` vacío, sin `#pragma warning disable` para tapar problemas.
- Imita el estilo del archivo vecino (nombres, densidad de comentarios, idioma de los mensajes).

## 4. Antes de dar por terminado un cambio

1. `dotnet build` del proyecto tocado (o de la solución si cambiaste contratos) → 0 errores, sin
   warnings nuevos.
2. Si tocaste un repo: re-verifica mapper ↔ `Include` (regla 2.1).
3. Si cambiaste un DTO público: ¿rompe al front? (extend-only, ver `api-design`).
4. `graphify update .` (regla del CLAUDE.md).
5. Reporta qué se verificó y qué no (p. ej. "compila; no probé el endpoint en vivo").
