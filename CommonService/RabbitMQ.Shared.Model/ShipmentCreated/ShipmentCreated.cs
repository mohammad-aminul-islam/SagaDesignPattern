
namespace CommonService.RabbitMQ.Shared.Model.ShipmentCreated;

public record ShipmentCreated(Guid OrderId, Guid ShipmentId, string TrackingNumber);
