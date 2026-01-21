using AgriProcurement.Procurement.Domain.Aggregates;

namespace AgriProcurement.Procurement.Domain.Repositories;

public interface IOrderRepository
{
    Task AddAsync(ProcurementOrder order, CancellationToken cancellationToken);
    Task<ProcurementOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
