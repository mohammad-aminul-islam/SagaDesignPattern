
using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Models;

namespace PaymentService.Infrastructure;

public class PaymentDbContext(DbContextOptions<PaymentDbContext> options) 
    : DbContext(options)
{
    public DbSet<Payment> Payments => Set<Payment>();
}
