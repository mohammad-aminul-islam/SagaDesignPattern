using OrderService.Application.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Infrastructure.Repositories;

public class OrderRepository(OrderDbContext context) : IOrderRepository
{
    public async Task<Order> AddAsync(Order order, CancellationToken ct = default)
    {
        await context.Orders.AddAsync(order, ct);
        await context.SaveChangesAsync(ct);
        return order;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Orders.FindAsync([id], ct);

    public async Task UpdateAsync(Order order, CancellationToken ct = default)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync(ct);
    }
}
