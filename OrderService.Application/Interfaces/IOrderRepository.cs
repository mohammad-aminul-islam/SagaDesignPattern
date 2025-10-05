using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order, CancellationToken ct = default);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task UpdateAsync(Order order, CancellationToken ct = default);
}
