namespace AgriProcurement.BuildingBlocks.Messaging.IntegrationEvents;

public sealed record ReserveSupplierIntegrationEvent(
    Guid OrderId
);
