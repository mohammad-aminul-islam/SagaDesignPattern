using MassTransit;
using OrderService.Application.Interfaces;

namespace OrderService.Infrastructure.Messaging;

public class MassTransitEventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
{
    public async Task PublishAsync<T>(T data, CancellationToken ct = default) where T : class
        => await publishEndpoint.Publish(data, ct);
}
