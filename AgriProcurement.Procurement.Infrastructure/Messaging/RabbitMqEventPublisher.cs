using System.Text.Json;
using AgriProcurement.Procurement.Application.Interfaces;
using AgriProcurement.Procurement.Infrastructure.Outbox;
using AgriProcurement.Procurement.Infrastructure.Persistence;

namespace AgriProcurement.Procurement.Infrastructure.Messaging;

public sealed class RabbitMqEventPublisher : IEventPublisher
{
    private readonly ProcurementDbContext _context;

    public RabbitMqEventPublisher(ProcurementDbContext context)
    {
        _context = context;
    }

    public async Task PublishAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken)
    {
        var payload = JsonSerializer.Serialize(@event);

        var outboxMessage = new OutboxMessage(
            @event!.GetType().Name,
            payload);

        await _context.OutboxMessages.AddAsync(
            outboxMessage,
            cancellationToken);
    }
}
