#!/usr/bin/env bash
# PreToolUse (Edit|Write|MultiEdit): when the target is a C# file, inject a short
# .NET / EF Core best-practices reminder into Claude's context. Never blocks.

input=$(cat)

file=$(printf '%s' "$input" | grep -o '"file_path"[[:space:]]*:[[:space:]]*"[^"]*"' | head -n1 | sed 's/.*"\([^"]*\)"$/\1/')

case "$file" in
  *.cs) ;;
  *) exit 0 ;;
esac

# Migrations are generated code: don't nag.
case "$file" in
  */Migrations/*) exit 0 ;;
esac

read -r -d '' msg <<'EOF'
[Trivo .NET guard] Editing C#. Before writing, apply skill trivo-dotnet-best-practices (load it if not loaded this session). Quick check:
- EF Core reads: AsNoTracking; Include EVERY navigation the mapper/DTO touches (incl. nested via ThenInclude — AsNoTracking does NOT fix up nav props across includes); AsSplitQuery with multiple collection Includes; project with Select for list endpoints; paginate; no N+1, no client-side joins.
- CQRS/Result: handler returns Result/ResultT, no try/catch for business rules, one SaveChangesAsync per command via IUnitOfWork, repo never saves.
- async all the way + CancellationToken last param and forwarded; no .Result/.Wait().
- NRT enabled: no unjustified '!'; sealed records for DTOs; structured logging (no string interpolation in log templates).
- After editing: dotnet build the touched project, then `graphify update .`.
EOF

# Escape for JSON (backslashes, quotes, newlines).
esc=$(printf '%s' "$msg" | sed -e 's/\\/\\\\/g' -e 's/"/\\"/g' | awk 'BEGIN{ORS="\\n"} {print}')

printf '{"hookSpecificOutput":{"hookEventName":"PreToolUse","additionalContext":"%s"}}\n' "$esc"
