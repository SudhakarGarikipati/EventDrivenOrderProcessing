using MapsterMapper;
using OrderService.Application.DTOs;
using OrderService.Application.Service.Abstraction;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Service.Implementation
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderServiceRepository _orderServiceRepository;
        private readonly IMapper _mapper;

        public OrderAppService(IOrderServiceRepository orderServiceRepository, IMapper mapper)
        {
            _orderServiceRepository = orderServiceRepository;
            _mapper = mapper;
        }

        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            var order = _mapper.Map<Domain.Entities.Order>(request);
            await _orderServiceRepository.CreateOrderAsync(order);
        }

        public async Task<GetOrderResponse> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderServiceRepository.GetOrderByIdAsync(orderId);
            var response = _mapper.Map<GetOrderResponse>(order);
            return response;
        }

        public Task UpdateOrderAsync(UpdateOrderRequest request)
        {
            var order = _mapper.Map<Domain.Entities.Order>(request);
            return _orderServiceRepository.UpdateOrderAsync(order);
        }
    }
}
