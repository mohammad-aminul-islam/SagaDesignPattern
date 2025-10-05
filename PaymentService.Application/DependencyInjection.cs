using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Commands;


namespace PaymentService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
          cfg.RegisterServicesFromAssemblyContaining<ProcessPaymentCommand>());
        return services;
    }
}

