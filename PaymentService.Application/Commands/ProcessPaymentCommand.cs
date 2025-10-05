using CommonService.RabbitMQ.Shared.Model.PaymentFailed;
using CommonService.RabbitMQ.Shared.Model.PaymentProcessed;
using MediatR;
using Microsoft.Extensions.Logging;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.Models;

namespace PaymentService.Application.Commands;

public record ProcessPaymentResult(bool Success, Guid? PaymentId, string? Error);
public record ProcessPaymentCommand(Guid OrderId, decimal Amount) : IRequest<ProcessPaymentResult>;

public class ProcessPaymentCommandHandler(
    IPaymentRepository paymentRepository,
    ILogger<ProcessPaymentCommandHandler> logger,
    IEventPublisher eventPublisher) : IRequestHandler<ProcessPaymentCommand, ProcessPaymentResult>
{
    public async Task<ProcessPaymentResult> Handle(ProcessPaymentCommand request, CancellationToken ct)
    {
        try
        {
            logger.LogInformation("💳 Processing payment for order {OrderId} - {Amount:C}",
               request.OrderId, request.Amount);

            if (Random.Shared.Next(100) < 10)
            {
                logger.LogWarning("💳 Payment FAILED - Insufficient funds");
                var failed = Payment.CreateFailed(request.OrderId, request.Amount);
                await paymentRepository.AddAsync(failed, ct);

                //Event emiting
                await eventPublisher.PublishAsync<PaymentFailed>(new PaymentFailed(request.OrderId,
                                                                                   "Payment FAILED - Insufficient funds"), ct);

                return new ProcessPaymentResult(false, null, "Insufficient funds");
            }

            var payment = Payment.CreateCompleted(request.OrderId, request.Amount);
            await paymentRepository.AddAsync(payment, ct);

            //Event emiting
            await eventPublisher.PublishAsync<PaymentProcessed>(new PaymentProcessed(request.OrderId, payment.Id, request.Amount), ct);

            logger.LogInformation("💳 Payment {PaymentId} completed ✓", payment.Id);
            return new ProcessPaymentResult(true, payment.Id, null);
        }
        catch (Exception ex)
        {
            await eventPublisher.PublishAsync<PaymentFailed>(new PaymentFailed(request.OrderId, ex.Message), ct);
            logger.LogError("{Message} {StackTrace}", ex.Message, ex.StackTrace);
            throw;
        }

    }
}
