
namespace InventoryService.Domain.Models;

public class Inventory
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal StockCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
