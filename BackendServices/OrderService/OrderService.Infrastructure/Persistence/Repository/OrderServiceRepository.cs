using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Persistence.Repository
{
    public class OrderServiceRepository : IOrderServiceRepository
    {
        private readonly OrderServiceDbContext _context;
        public OrderServiceRepository(OrderServiceDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            foreach (var item in order.OrderItems)
            {
                await _context.OrderItems.AddAsync(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            return order;
        }

        public async Task UpdateOrderAsync(Order order)
        {
             _context.Orders.Update(order);
              await _context.SaveChangesAsync();
        }
    }
}
