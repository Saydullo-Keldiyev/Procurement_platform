using Microsoft.EntityFrameworkCore;
using AgriProcurement.Procurement.Domain.Aggregates;
using AgriProcurement.Procurement.Domain.Repositories;
using AgriProcurement.Procurement.Infrastructure.Persistence;

namespace AgriProcurement.Procurement.Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly ProcurementDbContext _context;

    public OrderRepository(ProcurementDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        ProcurementOrder order,
        CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
    }

    public async Task<ProcurementOrder?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
