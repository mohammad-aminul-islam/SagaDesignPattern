using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
          cfg.RegisterServicesFromAssemblyContaining<CreateOrderCommand>());
        return services;
    }
}

