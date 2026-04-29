using Shared.Events;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderCreatedPublisher
    {
        Task PublishAsync(OrderCreated orderCreated);
    }
}
