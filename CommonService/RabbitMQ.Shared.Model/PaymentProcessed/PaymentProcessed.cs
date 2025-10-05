namespace CommonService.RabbitMQ.Shared.Model.PaymentProcessed;

public record PaymentProcessed(Guid OrderId, Guid PaymentId, decimal Amount);
