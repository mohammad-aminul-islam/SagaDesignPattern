using CommonService.RabbitMQ.Shared.Model.OrderCreated;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Interfaces;
using OrderService.Domain.Models;


namespace OrderService.Application.Commands;

public record CreateOrderCommand(decimal Amount, string CustomerEmail) : IRequest<Guid>;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IEventPublisher eventPublisher,
    ILogger<CreateOrderCommandHandler> logger) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken ct)
    {
        var order = new Order().Create(request.Amount, request.CustomerEmail);
        await orderRepository.AddAsync(order, ct);

        logger.LogInformation("📋 [COMMAND] Order {OrderId} created for {Amount:C}",
            order.Id, request.Amount);

        await eventPublisher.PublishAsync(
            new OrderCreated(order.Id, request.Amount, request.CustomerEmail), ct);

        return order.Id;
    }
}
