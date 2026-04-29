using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderService.Application.Service.Abstraction;
using OrderService.Infrastructure.Persistence;
using Shared.Events;

namespace OrderService.Infrastructure.Messaging
{
    public class PaymentCompletedConsumer : KafkaConsumerBase<PaymentCompleted>
    {
        private const string topic = "payment-completed";
        private const string groupId = "order-service-group";
        private readonly ILogger<PaymentCompletedConsumer> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public PaymentCompletedConsumer(IServiceScopeFactory scopeFactory, ILogger<PaymentCompletedConsumer> logger,IConfiguration configuration) : 
            base(configuration, topic, groupId)
        {
            _logger = logger;
            // Create a scope factory to resolve scoped services like IOrderAppService
            _scopeFactory = scopeFactory;
        }

        protected override async Task HandleMessageAsync(PaymentCompleted message)
        {
           _logger.LogInformation("Consuming PaymentCompleted event for OrderId={OrderId}", message.OrderId);
            // Create a new scope to resolve the IOrderAppService for this message
            var scope = _scopeFactory.CreateScope();
            // Resolve the IOrderAppService from the scope
            var orderAppService = scope.ServiceProvider.GetRequiredService<IOrderAppService>();
            await orderAppService.MarkOrderAsPaidAsync(message.OrderId, message.PaymentId, message.ProcessedAt);
           _logger.LogInformation("Completed PaymentCompleted event for OrderId={OrderId}", message.OrderId);
        }
    }
}
