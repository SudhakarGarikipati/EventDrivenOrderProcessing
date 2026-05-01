using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace PaymentService.Infrastructure.Messaging
{
    public abstract class KafkaConsumerBase<T> : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly string _topicName;
        private readonly ILogger<KafkaConsumerBase<T>> _logger;
        private readonly string _bootstrapServers;

        protected KafkaConsumerBase(IConfiguration configuration, string topic, string groupId, ILogger<KafkaConsumerBase<T>> logger)
        {
            _bootstrapServers = configuration["KafKa:BootstrapServers"];
            var consumerCofig = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _logger = logger;
            _topicName = topic;
            _consumer = new ConsumerBuilder<string, string>(consumerCofig).Build();
            //_consumer.Subscribe(topic);
        }

        protected abstract Task HandleMessageAsync(T message);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 1. Wait until topic exists
            while (!TopicExists(_topicName))
            {
                _logger.LogWarning("Kafka topic '{Topic}' not found. Retrying in 2 seconds...", _topicName);
                await Task.Delay(2000, stoppingToken);
            }

            _logger.LogInformation("Kafka topic '{Topic}' is available. Subscribing...", _topicName);
            // 2. Subscribe safely
            try
            {
                _consumer.Subscribe(_topicName);
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(stoppingToken);
                    _logger.LogInformation("Received message of size {Size} bytes",
            result.Message.Value?.Length ?? 0);
                    var message = JsonSerializer.Deserialize<T>(result.Message.Value);
                    await HandleMessageAsync(message);
                }
            }
            catch (OutOfMemoryException ex)
            {
                _logger.LogError(ex, "OOM inside consumer loop");
                throw;
            }
        }

        private bool TopicExists(string topicName)
        {
            try
            {
                var adminConfig = new AdminClientConfig { BootstrapServers = _bootstrapServers };

                using var admin = new AdminClientBuilder(adminConfig).Build();
                var metadata = admin.GetMetadata(TimeSpan.FromSeconds(5));

                return metadata.Topics.Any(t =>
                    t.Topic == topicName &&
                    t.Error.Code == ErrorCode.NoError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking Kafka topic existence");
                return false;
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }
}
