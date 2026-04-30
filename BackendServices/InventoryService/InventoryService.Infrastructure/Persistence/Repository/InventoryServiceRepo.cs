using InventoryService.Domain.Entities;
using InventoryService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventoryService.Infrastructure.Persistence.Repository
{
    internal class InventoryServiceRepo : IInventoryServiceRepo
    {
        private readonly CatalogServiceDbContext _dbContext;
        private readonly ILogger<InventoryServiceRepo> _logger;

        public InventoryServiceRepo(CatalogServiceDbContext dbContext, ILogger<InventoryServiceRepo> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Dictionary<int, bool>> CheckStock(Dictionary<int, int> productData)
        {
            _logger.LogInformation("Started check stock to validate the availability");
            return await _dbContext.Products.Where(p => productData.Keys.Contains(p.ProductId)).ToDictionaryAsync(p => p.ProductId, p => true);
        }

        public async Task<IEnumerable<Product>> GetProducts(List<int> productIds)
        {
            _logger.LogInformation("Started get products.");
            return await _dbContext.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync();
        }
    }
}
