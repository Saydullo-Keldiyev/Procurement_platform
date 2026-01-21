using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using AgriProcurement.Procurement.Application.Sagas;

namespace AgriProcurement.Procurement.Infrastructure.Messaging;

public sealed class SagaEventPublisher : ISagaEventPublisher
{
    private readonly IConnection _connection;

    public SagaEventPublisher(IConnection connection)
    {
        _connection = connection;
    }

    public Task PublishAsync<T>(
        T @event,
        CancellationToken cancellationToken)
    {
        using var channel = _connection.CreateModel();

        var body = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(@event));

        channel.BasicPublish(
            exchange: "",
            routingKey: "supplier-commands",
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }
}
