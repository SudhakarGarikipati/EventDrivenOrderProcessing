using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderServiceRepository
    {
        public Task CreateOrderAsync(Order order);

        public Task UpdateOrderAsync(Order order);

        public Task<Order> GetOrderByIdAsync(Guid orderId);
    }
}
