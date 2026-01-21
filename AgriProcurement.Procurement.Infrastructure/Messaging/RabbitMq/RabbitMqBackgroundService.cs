using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using AgriProcurement.Procurement.Infrastructure.Messaging.Consumers;

namespace AgriProcurement.Procurement.Infrastructure.Messaging.RabbitMq;

public sealed class RabbitMqBackgroundService : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly OrderCreatedConsumer _consumer;

    public RabbitMqBackgroundService(
        IConfiguration configuration,
        OrderCreatedConsumer consumer)
    {
        _configuration = configuration;
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var host = _configuration["RabbitMq:Host"];

        var connection =
            RabbitMqConnectionFactory.Create(host);

        await _consumer.StartAsync(
            connection,
            stoppingToken);
    }
}
