using Microsoft.Extensions.Configuration;
using OrderService.Infrastructure.Persistence;
using Shared.Events;

namespace OrderService.Infrastructure.Messaging
{
    public class PaymentCompletedConsumer : KafkaConsumerBase<PaymentCompleted>
    {
        private readonly OrderServiceDbContext _dbContext;
        private const string topic = "payment-completed";
        private const string groupId = "order-service-group";
        public PaymentCompletedConsumer(IConfiguration configuration, OrderServiceDbContext orderServiceDbContext) : 
            base(configuration, topic, groupId)
        {
            _dbContext = orderServiceDbContext;
        }

        protected override async Task HandleMessageAsync(PaymentCompleted message)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == message.OrderId);
            if (order != null)
            {
                order.PaymentId = message.PaymentId;
                order.AcceptDate = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
