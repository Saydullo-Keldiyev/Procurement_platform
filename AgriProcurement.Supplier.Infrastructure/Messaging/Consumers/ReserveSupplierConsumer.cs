using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using AgriProcurement.BuildingBlocks.Messaging.IntegrationEvents;
using AgriProcurement.Supplier.Application.Handlers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AgriProcurement.Supplier.Infrastructure.Messaging.Consumers;

public sealed class ReserveSupplierConsumer
{
    private const string QueueName = "supplier-commands";

    private readonly IServiceScopeFactory _scopeFactory;

    public ReserveSupplierConsumer(
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync(
        IConnection connection,
        CancellationToken cancellationToken)
    {
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += async (_, ea) =>
        {
            using var scope = _scopeFactory.CreateScope();
            var handler =
                scope.ServiceProvider.GetRequiredService<ReserveSupplierHandler>();

            var body = Encoding.UTF8.GetString(ea.Body.ToArray());
            var command =
                JsonSerializer.Deserialize<ReserveSupplierIntegrationEvent>(body)!;

            bool success = await handler.HandleAsync(
                command.OrderId,
                cancellationToken);

            IIntegrationEvent response = success
    ? new SupplierReservedIntegrationEvent(command.OrderId)
    : new SupplierReserveFailedIntegrationEvent(
        command.OrderId, "No capacity");

            var responseBody = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(response));

            channel.BasicPublish(
                exchange: "",
                routingKey: "procurement-events",
                basicProperties: null,
                body: responseBody);

            channel.BasicAck(ea.DeliveryTag, false);
        };

        channel.BasicConsume(
            QueueName,
            autoAck: false,
            consumer: consumer);

        await Task.CompletedTask;
    }
}
