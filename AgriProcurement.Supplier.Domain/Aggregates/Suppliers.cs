namespace AgriProcurement.Supplier.Domain.Aggregates;

public sealed class Suppliers
{
    public Guid Id { get; private set; }
    public int AvailableCapacity { get; private set; }

    public bool CanReserve() => AvailableCapacity > 0;

    public void Reserve()
    {
        if (!CanReserve())
            throw new InvalidOperationException("No capacity");

        AvailableCapacity--;
    }
}
