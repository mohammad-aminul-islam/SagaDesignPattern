using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Consumers;
using OrderService.Application.Interfaces;
using OrderService.Domain.Events;
using OrderService.Infrastructure.Messaging;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

        services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IEventPublisher, MassTransitEventPublisher>();

        // MassTransit with RabbitMQ
        services.AddMassTransit(x =>
        {
            // Add consumers here if this service consumes any events
             x.AddConsumer<PaymentFailedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration["RabbitMq:Host"]), h =>
                {
                    h.Username(configuration["RabbitMq:Username"]);
                    h.Password(configuration["RabbitMq:Password"]);
                });

                cfg.ReceiveEndpoint("payment-failed", e =>
                {
                    e.ConfigureConsumer<PaymentFailedConsumer>(context);
                });

                cfg.Message<OrderCreated>(config =>
                {
                    config.SetEntityName("order.created"); // custom exchange name
                });
            });
        });

        return services;
    }
}
