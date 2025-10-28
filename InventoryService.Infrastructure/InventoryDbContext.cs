using InventoryService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace InventoryService.Infrastructure;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {

    }

    public DbSet<Product> Products{ get; set; }
    public DbSet<Inventory> Inventories { get; set; }
}
