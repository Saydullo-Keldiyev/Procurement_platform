namespace AgriProcurement.BuildingBlocks.Messaging.IntegrationEvents;

public sealed record OrderCreatedIntegrationEvent(
    Guid OrderId,
    DateTime OccurredOn
);
