using AgriProcurement.Procurement.Application.Interfaces;
using AgriProcurement.Procurement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AgriProcurement.Procurement.Infrastructure.Idempotency;

public sealed class EfIdempotencyService : IIdempotencyService
{
    private readonly ProcurementDbContext _context;

    public EfIdempotencyService(ProcurementDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsProcessedAsync(
        string key,
        CancellationToken cancellationToken)
    {
        return await _context.Set<IdempotentRequest>()
            .AnyAsync(x => x.Key == key, cancellationToken);
    }

    public async Task MarkAsProcessedAsync(
        string key,
        CancellationToken cancellationToken)
    {
        _context.Add(new IdempotentRequest(key));
        await _context.SaveChangesAsync(cancellationToken);
    }
}

internal sealed class IdempotentRequest
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Key { get; private set; }

    private IdempotentRequest() { }
    public IdempotentRequest(string key) => Key = key;
}
