using PaymentService.Domain.Enums;

namespace PaymentService.Domain.Models;

public class Payment
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime ProcessedAt { get; private set; }

    private Payment() { }

    public static Payment CreateCompleted(Guid orderId, decimal amount) => new()
    {
        Id = Guid.NewGuid(),
        OrderId = orderId,
        Amount = amount,
        Status = PaymentStatus.Completed,
        ProcessedAt = DateTime.UtcNow
    };

    public static Payment CreateFailed(Guid orderId, decimal amount) => new()
    {
        Id = Guid.NewGuid(),
        OrderId = orderId,
        Amount = amount,
        Status = PaymentStatus.Failed,
        ProcessedAt = DateTime.UtcNow
    };

    public void Refund()
    {
        if (Status != PaymentStatus.Completed)
            throw new InvalidOperationException("Can only refund completed payments");
        Status = PaymentStatus.Refunded;
    }
}
