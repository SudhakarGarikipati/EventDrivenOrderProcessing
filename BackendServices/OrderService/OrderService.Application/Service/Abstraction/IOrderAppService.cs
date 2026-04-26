using OrderService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Service.Abstraction
{
    public interface IOrderAppService
    {
            public Task CreateOrderAsync(CreateOrderRequest request);
    
            public Task UpdateOrderAsync(UpdateOrderRequest request);
    
            public Task<GetOrderResponse> GetOrderByIdAsync(Guid orderId);
    }
}
