using OrderService.Application.DTOs;

namespace OrderService.Application.Service.Abstraction
{
    public interface IOrderAppService
    {
            public Task CreateOrderAsync(CreateOrderRequest request);
    
            public Task UpdateOrderAsync(UpdateOrderRequest request);
    
            public Task<GetOrderResponse> GetOrderByIdAsync(Guid orderId);
    }
}
