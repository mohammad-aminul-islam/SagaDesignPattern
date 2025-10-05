using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Interfaces;
using OrderService.Domain.Enums;

namespace OrderService.Application.Commands;

public record UpdateOrderStatusCommand(Guid OrderId, OrderStatus Status) : IRequest;

public class UpdateOrderStatusCommandHandler(
    IOrderRepository orderRepository,
    ILogger<UpdateOrderStatusCommandHandler> logger) : IRequestHandler<UpdateOrderStatusCommand>
{
    public async Task Handle(UpdateOrderStatusCommand request, CancellationToken ct)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, ct);
        if (order == null)
        {
            logger.LogWarning("📋 Order {OrderId} not found", request.OrderId);
            return;
        }

        order.UpdateStatus(request.Status);
        await orderRepository.UpdateAsync(order, ct);

        logger.LogInformation("📋 Order {OrderId} → {Status}", request.OrderId, request.Status);
    }
}
