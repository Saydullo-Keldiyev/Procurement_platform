using AgriProcurement.Supplier.Application.Handlers;
using AgriProcurement.Supplier.Infrastructure.Messaging.Consumers;
using AgriProcurement.Supplier.Infrastructure.Messaging.RabbitMq;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;

namespace AgriProcurement.Supplier.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSupplierModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 🔹 Application
        services.AddScoped<ReserveSupplierHandler>();

        // 🔹 RabbitMQ consumer
        services.AddSingleton<ReserveSupplierConsumer>();

        // 🔹 Background worker (RabbitMQ listener)
        services.AddHostedService<RabbitMqBackgroundService>();

        // 🔹 RabbitMQ connection
        services.AddSingleton<IConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var host = configuration["RabbitMq:Host"];
            if (string.IsNullOrWhiteSpace(host))
                throw new InvalidOperationException(
                    "RabbitMq:Host configuration is missing");

            var factory = new ConnectionFactory
            {
                HostName = host,
                Port = int.Parse(configuration["RabbitMq:Port"] ?? "5672"),
                UserName = "guest",
                Password = "guest",
                DispatchConsumersAsync = true
            };

            return factory.CreateConnection();
        });

        return services;
    }
}
