using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.Enums;
using PaymentService.Domain.Models;

namespace PaymentService.Infrastructure.Repositories;

public class PaymentRepository(PaymentDbContext context) : IPaymentRepository
{
    public async Task<Payment> AddAsync(Payment payment, CancellationToken ct = default)
    {
        await context.Payments.AddAsync(payment, ct);
        await context.SaveChangesAsync(ct);
        return payment;
    }

    public async Task<Payment?> GetByOrderIdAsync(Guid orderId, CancellationToken ct = default)
        => await context.Payments
            .FirstOrDefaultAsync(p => p.OrderId == orderId && p.Status == PaymentStatus.Completed, ct);

    public async Task UpdateAsync(Payment payment, CancellationToken ct = default)
    {
        context.Payments.Update(payment);
        await context.SaveChangesAsync(ct);
    }
}
