using RabbitMQ.Client;

namespace AgriProcurement.Procurement.Infrastructure.Messaging.RabbitMq;

public static class RabbitMqConnectionFactory
{
    public static IConnection Create(string hostName)
    {
        var factory = new ConnectionFactory
        {
            HostName = hostName,
            DispatchConsumersAsync = true
        };

        return factory.CreateConnection();
    }
}
