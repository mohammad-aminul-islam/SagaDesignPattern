using CommonService.RabbitMQ.Shared.Model.OrderCreated;
using MassTransit;
using MediatR;
using PaymentService.Application.Commands;

namespace PaymentService.Application.Consumers;

public class OrderCreatedConsumer(IMediator mediator) : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
        => await mediator.Send(
            new ProcessPaymentCommand(context.Message.OrderId, context.Message.Amount),
            context.CancellationToken);
}
