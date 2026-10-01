## graphify

This project has a knowledge graph at graphify-out/ with god nodes, community structure, and cross-file relationships.

Rules:
- For codebase questions, first run `graphify query "<question>"` when graphify-out/graph.json exists. Use `graphify path "<A>" "<B>"` for relationships and `graphify explain "<concept>"` for focused concepts. These return a scoped subgraph, usually much smaller than GRAPH_REPORT.md or raw grep output.
- If graphify-out/wiki/index.md exists, use it for broad navigation instead of raw source browsing.
- Read graphify-out/GRAPH_REPORT.md only for broad architecture review or when query/path/explain do not surface enough context.
- After modifying code, run `graphify update .` to keep the graph current (AST-only, no API cost).

## .NET / EF Core best practices (always on)

Stack: .NET 8 / C# 12, NRT enabled, EF Core + PostgreSQL, MediatR CQRS, FluentValidation, Result pattern.

Rules:
- Before writing, fixing or reviewing ANY `.cs` file — including one-line bugfixes — load the `trivo-dotnet-best-practices` skill and follow its routing table to the detailed skill (`efcore-patterns`, `database-performance`, `dotnet-endpoint-architecture`, etc.).
- EF Core: when touching a repository query, open the mapper that consumes the entity and make sure every navigation it reads has an `Include`/`ThenInclude` (with `AsNoTracking` there is no fix-up across include paths). `AsNoTracking` for reads, `AsSplitQuery` for multiple collection includes, no N+1, no in-memory joins.
- Handlers return `Result`/`ResultT` (no business try/catch); one `SaveChangesAsync` per command via `IUnitOfWork`; repositories never save. `CancellationToken` last and forwarded.
- Done means: `dotnet build` of the touched project passes with no new warnings, then `graphify update .`; report what was and wasn't verified.
- For a second opinion on a non-trivial C# diff, use the `dotnet-reviewer` agent.
