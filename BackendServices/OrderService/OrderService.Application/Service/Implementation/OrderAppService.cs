using MapsterMapper;
using OrderService.Application.DTOs;
using OrderService.Application.Service.Abstraction;
using OrderService.Domain.Interfaces;
using Shared.Events;

namespace OrderService.Application.Service.Implementation
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderServiceRepository _orderServiceRepository;
        private readonly IMapper _mapper;
        private readonly IOrderCreatedPublisher _orderCreatedPublisher;

        public OrderAppService(IOrderServiceRepository orderServiceRepository, IMapper mapper, IOrderCreatedPublisher orderCreatedPublisher)
        {
            _orderServiceRepository = orderServiceRepository;
            _mapper = mapper;
            _orderCreatedPublisher = orderCreatedPublisher;
        }

        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            var order = _mapper.Map<Domain.Entities.Order>(request);
            order.CreatedDate = DateTime.UtcNow; // ISO 8601
            await _orderServiceRepository.CreateOrderAsync(order);
            var createdOrder = await _orderServiceRepository.GetOrderByIdAsync(order.OrderId);
            var orderCreated = _mapper.Map<OrderCreated>(createdOrder);
            orderCreated.TotalAmount = createdOrder.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
            await _orderCreatedPublisher.PublishAsync(orderCreated);
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
