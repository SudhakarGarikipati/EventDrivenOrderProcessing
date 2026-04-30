using InventoryService.Application.Mappings;
using InventoryService.Application.Service.Abstraction;
using InventoryService.Application.Service.Implementation;
using InventoryService.Domain.Interfaces;
using InventoryService.Infrastructure.Persistence;
using InventoryService.Infrastructure.Persistence.Repository;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure
{
    public static class InventoryServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogServiceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("InventoryServiceConnectionString"));
            });

            // Register other services
            services.AddScoped<IInventoryServiceRepo, InventoryServiceRepo>();
            services.AddScoped<IInventoryAppService, InventoryAppService>();

            // Mapper
            var config = new TypeAdapterConfig();
            config.Scan(typeof(StockMappingConfig).Assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
