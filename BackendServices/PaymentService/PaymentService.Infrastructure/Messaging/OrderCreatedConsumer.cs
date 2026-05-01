using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentService.Application.Service.Abstraction;
using Shared.Events;

namespace PaymentService.Infrastructure.Messaging
{
    public class OrderCreatedConsumer : KafkaConsumerBase<OrderCreated>
    {
        private const string topic = "order-created";
        private const string groupId = "payment-service-group";
        private readonly ILogger<OrderCreatedConsumer> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderCreatedConsumer(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<OrderCreatedConsumer> logger) : base(configuration, topic, groupId, logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task HandleMessageAsync(OrderCreated message)
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = message.CorrelationId,
                ["OrderId"] = message.OrderId
            }))
            {
                _logger.LogInformation("Consuming OrderCreated event for OrderId={OrderId}", message.OrderId);
                using var scope = _scopeFactory.CreateScope();
                var paymentAppService = scope.ServiceProvider.GetRequiredService<IPaymentAppService>();
                await paymentAppService.CreatePaymentAsync(message.OrderId, message.CustomerId, message.CartId, message.TotalAmount);
                _logger.LogInformation("Completed OrderCreated, Created Payment event for OrderId={OrderId}", message.OrderId);
            }
        }
    }
}
