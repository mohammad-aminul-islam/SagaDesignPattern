
namespace CommonService.RabbitMQ.Shared.Model.ShipmentFailed;

public record ShipmentFailed(Guid OrderId, string Reason);
