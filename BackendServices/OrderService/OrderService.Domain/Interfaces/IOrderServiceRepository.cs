using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderServiceRepository
    {
        public Task CreateOrderAsync(Order order);

        public Task UpdateOrderAsync(Order order);

        public Task<Order> GetOrderByIdAsync(Guid orderId);
    }
}
