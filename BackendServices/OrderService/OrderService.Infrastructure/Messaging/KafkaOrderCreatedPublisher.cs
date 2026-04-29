using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderService.Domain.Interfaces;
using Shared.Events;
using System.Text.Json;

namespace OrderService.Infrastructure.Messaging
{
    public class KafkaOrderCreatedPublisher : IOrderCreatedPublisher
    {
        private readonly IProducer<string,string> _producer;
        private readonly ILogger<KafkaOrderCreatedPublisher> _logger;

        public KafkaOrderCreatedPublisher(IConfiguration configuration, ILogger<KafkaOrderCreatedPublisher> logger )
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["KafKa:BootstrapServers"]
            };
            _producer = new ProducerBuilder<string, string>(producerConfig).Build();
            _logger = logger;

        }

        public async Task PublishAsync(OrderCreated orderCreated)
        {
            _logger.LogInformation("Publishing OrderCreated event for OrderId={OrderId}", orderCreated.OrderId);
            var message = new Message<string, string>
            {
                Key = orderCreated.OrderId.ToString(),
                Value = JsonSerializer.Serialize(orderCreated)
            };
            await _producer.ProduceAsync("order-created", message);
            _logger.LogInformation("Publishing OrderCreated event for OrderId={OrderId}", orderCreated.OrderId);
        }
    }
}
