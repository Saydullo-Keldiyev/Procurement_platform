using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AgriProcurement.BuildingBlocks.Messaging.IntegrationEvents;
using AgriProcurement.Procurement.Application.Sagas;

namespace AgriProcurement.Procurement.Infrastructure.Messaging.Consumers;

public sealed class OrderCreatedConsumer
{
    private const string QueueName = "procurement-events";

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(
        IServiceScopeFactory scopeFactory,
        ILogger<OrderCreatedConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(
        IConnection connection,
        CancellationToken cancellationToken)
    {
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += async (_, ea) =>
        {
            try
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                var integrationEvent =
                    JsonSerializer.Deserialize<OrderCreatedIntegrationEvent>(body);

                if (integrationEvent == null)
                    throw new InvalidOperationException("Invalid event payload");

                using var scope = _scopeFactory.CreateScope();
                var saga = scope.ServiceProvider
                    .GetRequiredService<ProcurementOrderSaga>();

                await saga.Handle(
                    integrationEvent,
                    cancellationToken);

                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error processing OrderCreated event");

                // ❗ message yo‘qolmasin
                channel.BasicNack(
                    ea.DeliveryTag,
                    false,
                    requeue: true);
            }
        };

        channel.BasicConsume(
            queue: QueueName,
            autoAck: false,
            consumer: consumer);

        await Task.CompletedTask;
    }
}
