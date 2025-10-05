using CommonService.RabbitMQ.Shared.Model.PaymentFailed;
using MassTransit;
using MediatR;
using OrderService.Application.Commands;
using OrderService.Domain.Enums;

namespace OrderService.Application.Consumers;

public class PaymentFailedConsumer(IMediator mediator) : IConsumer<PaymentFailed>
{
    public async Task Consume(ConsumeContext<PaymentFailed> context)
        => await mediator.Send(
            new UpdateOrderStatusCommand(context.Message.OrderId, OrderStatus.Failed),
            context.CancellationToken);
}
