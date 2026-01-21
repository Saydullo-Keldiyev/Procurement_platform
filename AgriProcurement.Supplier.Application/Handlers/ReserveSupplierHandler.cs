using AgriProcurement.Supplier.Domain.Aggregates;

namespace AgriProcurement.Supplier.Application.Handlers;

public sealed class ReserveSupplierHandler
{
    public async Task<bool> HandleAsync(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        // DEMO LOGIC (keyin DB bo‘ladi)
        var supplier = new Suppliers
        {
           // AvailableCapacity = 1
        };

        if (!supplier.CanReserve())
            return false;

        supplier.Reserve();
        return true;
    }
}
