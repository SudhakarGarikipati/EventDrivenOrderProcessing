using InventoryService.Domain.Entities;

namespace InventoryService.Domain.Interfaces
{
    public interface IInventoryServiceRepo
    {
        Task<Dictionary<int,bool>> CheckStock(Dictionary<int, int> productData);

        Task<IEnumerable<Product>> GetProducts(List<int> productIds);
    }
}
