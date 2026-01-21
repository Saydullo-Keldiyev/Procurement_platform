namespace AgriProcurement.Procurement.Domain.Events;

public sealed record OrderCreatedDomainEvent(
    Guid OrderId,
    DateTime OccurredOn
);
