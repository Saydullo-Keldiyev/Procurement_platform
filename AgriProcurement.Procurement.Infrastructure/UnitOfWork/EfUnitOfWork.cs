using AgriProcurement.Procurement.Application.Interfaces;
using AgriProcurement.Procurement.Infrastructure.Persistence;

namespace AgriProcurement.Procurement.Infrastructure.UnitOfWork;

public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly ProcurementDbContext _context;

    public EfUnitOfWork(ProcurementDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
