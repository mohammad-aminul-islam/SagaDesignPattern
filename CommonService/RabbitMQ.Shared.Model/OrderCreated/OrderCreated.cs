
namespace CommonService.RabbitMQ.Shared.Model.OrderCreated;

public record OrderCreated(Guid OrderId, decimal Amount, string CustomerEmail);
