using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using AgriProcurement.Supplier.Infrastructure.Messaging.Consumers;

namespace AgriProcurement.Supplier.Infrastructure.Messaging.RabbitMq;

public sealed class RabbitMqBackgroundService : BackgroundService
{
    private readonly IConnection _connection;
    private readonly ReserveSupplierConsumer _consumer;

    public RabbitMqBackgroundService(
        IConnection connection,
        ReserveSupplierConsumer consumer)
    {
        _connection = connection;
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        await _consumer.StartAsync(
            _connection,
            stoppingToken);
    }
}
