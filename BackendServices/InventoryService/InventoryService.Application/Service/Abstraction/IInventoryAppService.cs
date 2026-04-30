using InventoryService.Application.DTOs;

namespace InventoryService.Application.Service.Abstraction
{
    public interface IInventoryAppService
    {
        Task<Dictionary<int, bool>> CheckStockAvailability(Dictionary<int, int> productData);

        Task<IEnumerable<ProductDto>> GetProducts(List<int> productIds);
    }
}
