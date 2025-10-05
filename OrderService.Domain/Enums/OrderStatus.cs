
namespace OrderService.Domain.Enums;

public enum OrderStatus
{
    Created = 1,
    PaymentPending = 2,
    PaymentCompleted = 3,
    ShipmentPending = 4,
    Completed = 5,
    Cancelled = 6,
    Failed = 7
}
