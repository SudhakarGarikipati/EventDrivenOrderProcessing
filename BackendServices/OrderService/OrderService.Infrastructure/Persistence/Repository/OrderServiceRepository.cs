using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

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
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == orderId);
            return order;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task MarkOrderAsPaidAsync(Order order, string paymentId, DateTime dateTime)
        {
            order.PaymentId = paymentId;
            order.AcceptDate = dateTime;
            await _context.SaveChangesAsync();
        }
    }
}
