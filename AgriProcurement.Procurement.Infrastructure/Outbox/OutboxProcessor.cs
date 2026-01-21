using Microsoft.EntityFrameworkCore;
using System.Text;
using RabbitMQ.Client;
using AgriProcurement.Procurement.Infrastructure.Persistence;

namespace AgriProcurement.Procurement.Infrastructure.Outbox;

public sealed class OutboxProcessor
{
    private readonly ProcurementDbContext _context;
    private readonly IConnection _connection;

    public OutboxProcessor(
        ProcurementDbContext context,
        IConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken)
    {
        var messages = await _context.OutboxMessages
            .Where(x => x.ProcessedOn == null)
            .OrderBy(x => x.OccurredOn)
            .Take(20)
            .ToListAsync(cancellationToken);

        if (!messages.Any())
            return;

        using var channel = _connection.CreateModel();
        channel.QueueDeclare("procurement-events",
            durable: true,
            exclusive: false);

        foreach (var message in messages)
        {
            var body = Encoding.UTF8.GetBytes(message.Payload);

            channel.BasicPublish(
                exchange: "",
                routingKey: "procurement-events",
                basicProperties: null,
                body: body);

            message.MarkProcessed();
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
