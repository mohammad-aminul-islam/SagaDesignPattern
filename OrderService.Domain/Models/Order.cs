using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Order Create(decimal amount, string customerEmail)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be positive", nameof(amount));
        if (string.IsNullOrWhiteSpace(customerEmail))
            throw new ArgumentException("Email is required", nameof(customerEmail));

        return new Order
        {
            Id = Guid.NewGuid(),
            Amount = amount,
            CustomerEmail = customerEmail,
            Status = OrderStatus.Created,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}
