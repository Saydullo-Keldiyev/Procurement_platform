namespace AgriProcurement.BuildingBlocks.Messaging.IntegrationEvents;

public sealed record SupplierReserveFailedIntegrationEvent(
    Guid OrderId,
    string Reason
) : IIntegrationEvent;
