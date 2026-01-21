using Microsoft.Extensions.Logging;
using AgriProcurement.BuildingBlocks.Messaging.IntegrationEvents;

namespace AgriProcurement.Procurement.Application.Sagas;

public sealed class ProcurementOrderSaga
{
    private readonly ILogger<ProcurementOrderSaga> _logger;
    private readonly ISagaEventPublisher _publisher;

    public ProcurementOrderSaga(
        ILogger<ProcurementOrderSaga> logger,
        ISagaEventPublisher publisher)
    {
        _logger = logger;
        _publisher = publisher;
    }

    public async Task Handle(
        OrderCreatedIntegrationEvent @event,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Saga started. OrderId: {OrderId}",
            @event.OrderId);

        // 🔥 SAGA STEP 1: Supplier Reserve
        await _publisher.PublishAsync(
            new ReserveSupplierIntegrationEvent(@event.OrderId),
            cancellationToken);
    }

    public async Task Handle(
        SupplierReservedIntegrationEvent @event,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Supplier reserved. OrderId: {OrderId}",
            @event.OrderId);

        // 🔜 NEXT STEP (keyin):
        // Payment initiation
        await Task.CompletedTask;
    }

    public async Task Handle(
        SupplierReserveFailedIntegrationEvent @event,
        CancellationToken cancellationToken)
    {
        _logger.LogWarning(
            "Supplier reserve FAILED. OrderId: {OrderId}, Reason: {Reason}",
            @event.OrderId,
            @event.Reason);

        // 🔥 COMPENSATION
        // Cancel order
        await Task.CompletedTask;
    }
}
