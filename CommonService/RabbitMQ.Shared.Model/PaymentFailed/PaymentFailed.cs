
namespace CommonService.RabbitMQ.Shared.Model.PaymentFailed;

public record PaymentFailed(Guid OrderId, string Reason);
