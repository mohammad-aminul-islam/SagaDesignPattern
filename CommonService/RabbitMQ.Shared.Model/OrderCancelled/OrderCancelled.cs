
namespace CommonService.RabbitMQ.Shared.Model.OrderCancelled;

public record OrderCancelled(Guid OrderId, string Reason);
