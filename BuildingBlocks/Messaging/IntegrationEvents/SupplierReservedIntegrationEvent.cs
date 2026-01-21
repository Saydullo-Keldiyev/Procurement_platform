namespace AgriProcurement.BuildingBlocks.Messaging.IntegrationEvents;

public sealed record SupplierReservedIntegrationEvent(
    Guid OrderId
) : IIntegrationEvent;
