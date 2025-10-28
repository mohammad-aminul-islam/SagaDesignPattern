
namespace InventoryService.Application.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync<T>(T data, CancellationToken ct = default) where T : class;
}
