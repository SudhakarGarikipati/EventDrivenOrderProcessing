using Mapster;
using OrderService.Application.DTOs;
using OrderService.Domain.Entities;

namespace OrderService.Application.Mappings
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, GetOrderResponse>();
            config.NewConfig<CreateOrderRequest, Order>();
            config.NewConfig<UpdateOrderRequest, Order>();
        }
    }
}
