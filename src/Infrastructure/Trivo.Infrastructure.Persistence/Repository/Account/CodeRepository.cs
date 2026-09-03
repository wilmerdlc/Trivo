using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository.Account;

public class CodeRepository(TrivoContext context) : GenericRepository<Code>(context), ICodeRepository
{
    public async Task CreateAsync(Code code, CancellationToken cancellationToken)
    {
        await Context.Set<Code>().AddAsync(code, cancellationToken);
    }

    public async Task<Code?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await Context.Set<Code>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CodeId == id, cancellationToken);

    public async Task<Code?> FindAsync(string code, CancellationToken cancellationToken) =>
        await Context.Set<Code>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Value == code, cancellationToken);

    public async Task DeleteAsync(Code code, CancellationToken cancellationToken)
    {
        Context.Set<Code>().Remove(code);
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(string code, CancellationToken cancellationToken) =>
        await ValidateAsync(c => c.Value == code, cancellationToken);

    public async Task<bool> IsValidAsync(string code, CancellationToken cancellationToken) =>
        await ValidateAsync(c => c.Value == code &&
                                 c.ExpiresAt > DateTime.UtcNow &&
                                 (bool)c.IsUsed!, cancellationToken);

    public async Task MarkAsUsedAsync(string code, CancellationToken cancellationToken)
    {
        var userCode = await Context.Set<Code>()
            .FirstOrDefaultAsync(c => c.Value == code, cancellationToken);

        if (userCode != null)
        {
            userCode.IsUsed = true;
            Context.Set<Code>().Update(userCode);
        }
    }

    public async Task<bool> IsUnusedAsync(string code, CancellationToken cancellationToken) =>
        await ValidateAsync(c => c.Value == code && (bool)(!c.IsUsed)!, cancellationToken);
}