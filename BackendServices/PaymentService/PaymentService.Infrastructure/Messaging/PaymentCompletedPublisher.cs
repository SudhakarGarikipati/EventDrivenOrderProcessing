using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentService.Domain.Interfaces;
using Shared.Events;
using System.Text.Json;

namespace PaymentService.Infrastructure.Messaging
{
    public class PaymentCompletedPublisher : IPaymentCompletedPublisher
    {
        private readonly ILogger<PaymentCompletedPublisher> _logger;
        private readonly IProducer<string, string> _producer;

        public PaymentCompletedPublisher(ILogger<PaymentCompletedPublisher> logger, IConfiguration configuration) {

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };

            _logger = logger;
            _producer = new  ProducerBuilder<string, string>(producerConfig).Build();
        }

        public async Task PublishAsync(PaymentCompleted paymentCompleted)
        {
            _logger.LogInformation("Publishing Payment Completed event for OrderId={OrderId}", paymentCompleted.OrderId);
            var message = new Message<string, string>
            {
                Key = paymentCompleted.PaymentId,
                Value = JsonSerializer.Serialize(paymentCompleted)
            };
            await _producer.ProduceAsync("payment-completed", message);
            _logger.LogInformation("Published Payment Completed event for OrderId={OrderId}", paymentCompleted.OrderId);
        }
    }
}
