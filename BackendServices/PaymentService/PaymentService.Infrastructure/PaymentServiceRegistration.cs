using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Mappings;
using PaymentService.Application.Service.Abstraction;
using PaymentService.Application.Service.Implementation;
using PaymentService.Domain.Interfaces;
using PaymentService.Infrastructure.Messaging;
using PaymentService.Infrastructure.Persistence;
using PaymentService.Infrastructure.Persistence.Repository;

namespace PaymentService.Infrastructure
{
    public static class PaymentServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentServiceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PaymentServiceConnectionString"));
            });

            // Add other service registrations as needed
            services.AddScoped<IPaymentServiceRepository, PaymentServiceRepository>();
            services.AddScoped<IPaymentAppService, PaymentAppService>();

            // Register Kafka consumer as a hosted service
            services.AddHostedService<OrderCreatedConsumer>();

            // register Kafka producer
            services.AddScoped<IPaymentCompletedPublisher, PaymentCompletedPublisher>();

            //Mapper registration
            var config = new TypeAdapterConfig();
            config.Scan(typeof(PaymentServiceMappings).Assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
