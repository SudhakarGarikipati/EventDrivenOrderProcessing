using InventoryService.Application.DTOs;
using InventoryService.Application.Service.Abstraction;
using InventoryService.Domain.Interfaces;
using MapsterMapper;

namespace InventoryService.Application.Service.Implementation
{
    public class InventoryAppService : IInventoryAppService
    {
        private readonly IInventoryServiceRepo _inventoryServiceRepo;
        private readonly IMapper _mapper;

        public InventoryAppService(IInventoryServiceRepo inventoryServiceRepo, IMapper mapper)
        {
            _inventoryServiceRepo = inventoryServiceRepo;
            _mapper = mapper;
        }

        public async Task<Dictionary<int, bool>> CheckStockAvailability(Dictionary<int, int> productData)
        {
           var availibityData = await _inventoryServiceRepo.CheckStock(productData);
            return availibityData;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(List<int> productIds)
        {
            var products = await _inventoryServiceRepo.GetProducts(productIds);
            var resDto = _mapper.Map<List<ProductDto>>(products);
            return resDto;
        }
    }
}
