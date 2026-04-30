using InventoryService.Application.DTOs;
using InventoryService.Domain.Entities;
using Mapster;

namespace InventoryService.Application.Mappings
{
    public class StockMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductDto>();
        }
    }
}
