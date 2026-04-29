using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace OrderService.Infrastructure.Messaging
{
    public abstract class KafkaConsumerBase<T> : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;

        protected KafkaConsumerBase(IConfiguration configuration, string topic, string groupId)
        {
            var consumerCofig = new ConsumerConfig
            {
                BootstrapServers = configuration["KafKa:BootstrapServers"],
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
             _consumer = new ConsumerBuilder<string,string>(consumerCofig).Build();
            _consumer.Subscribe(topic);
        }

        protected abstract Task HandleMessageAsync(T message);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);
                var message = JsonSerializer.Deserialize<T>(result.Message.Value);
                await HandleMessageAsync(message);
            }
        }
    }
}
