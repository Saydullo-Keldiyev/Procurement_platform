using AgriProcurement.Procurement.Domain.Entities;
using AgriProcurement.Procurement.Domain.Enums;
using AgriProcurement.Procurement.Domain.Events;

namespace AgriProcurement.Procurement.Domain.Aggregates;

public class ProcurementOrder
{
    private readonly List<OrderLine> _orderLines = new();

    public Guid Id { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines.AsReadOnly();

    private ProcurementOrder() { }

    public ProcurementOrder(Guid id)
    {
        Id = id;
        Status = OrderStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddLine(OrderLine line)
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException("Cannot modify order after creation");

        _orderLines.Add(line);
    }

    public OrderCreatedDomainEvent Create()
    {
        if (!_orderLines.Any())
            throw new InvalidOperationException("Order must have at least one line");

        Status = OrderStatus.Created;

        return new OrderCreatedDomainEvent(Id, DateTime.UtcNow);
    }
}
