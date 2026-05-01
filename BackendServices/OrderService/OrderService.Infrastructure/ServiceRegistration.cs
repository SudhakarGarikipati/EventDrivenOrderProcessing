using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Mappings;
using OrderService.Application.Service.Abstraction;
using OrderService.Application.Service.Implementation;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.gRPC;
using OrderService.Infrastructure.Messaging;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Persistence.Repository;

namespace OrderService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderServiceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderServiceConnectionString")));

            services.AddScoped<IOrderAppService,OrderAppService>();
            services.AddScoped<IOrderServiceRepository,OrderServiceRepository>();

            // Register Kafka producer
            services.AddScoped<IOrderCreatedPublisher, KafkaOrderCreatedPublisher>();
            services.AddHostedService<PaymentCompletedConsumer>();

            //
            services.AddScoped<IInventoryService, InventoryServiceClient>();

            // Add other service registrations as needed
            var config = new TypeAdapterConfig();
            config.Scan(typeof(MappingConfig).Assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
