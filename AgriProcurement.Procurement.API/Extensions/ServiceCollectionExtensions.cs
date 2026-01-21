using AgriProcurement.Procurement.API.Filters;
using AgriProcurement.Procurement.Application.Commands.CreateOrder;
using AgriProcurement.Procurement.Application.Interfaces;
using AgriProcurement.Procurement.Application.Sagas;
using AgriProcurement.Procurement.Domain.Repositories;
using AgriProcurement.Procurement.Infrastructure.Idempotency;
using AgriProcurement.Procurement.Infrastructure.Messaging;
using AgriProcurement.Procurement.Infrastructure.Messaging.Consumers;
using AgriProcurement.Procurement.Infrastructure.Messaging.RabbitMq;
using AgriProcurement.Procurement.Infrastructure.Persistence;
using AgriProcurement.Procurement.Infrastructure.Repositories;
using AgriProcurement.Procurement.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RabbitMQ.Client;

namespace AgriProcurement.Procurement.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProcurementModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 🔹 PostgreSQL DbContext
        services.AddDbContext<ProcurementDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Postgres"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(
                        typeof(ProcurementDbContext).Assembly.FullName);
                }));

        // 🔹 Repository
        services.AddScoped<IOrderRepository, OrderRepository>();

        // 🔹 Application services
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<IEventPublisher, RabbitMqEventPublisher>();
        services.AddScoped<IIdempotencyService, EfIdempotencyService>();
        services.AddScoped<ProcurementOrderSaga>();
        services.AddSingleton<OrderCreatedConsumer>();
        services.AddHostedService<RabbitMqBackgroundService>();
        services.AddScoped<ISagaEventPublisher, SagaEventPublisher>();


        // 🔹 Handlers
        services.AddScoped<CreateOrderCommandHandler>();

        // 🔹 Filters
        services.AddScoped<IdempotencyFilter>();

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
