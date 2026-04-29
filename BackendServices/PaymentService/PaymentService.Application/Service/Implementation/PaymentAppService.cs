using MapsterMapper;
using PaymentService.Application.DTOs;
using PaymentService.Application.Service.Abstraction;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Application.Service.Implementation
{
    public class PaymentAppService : IPaymentAppService
    {
        private readonly IPaymentServiceRepository _paymentServiceRepository;
        private readonly IMapper _mapper;

        public PaymentAppService(IPaymentServiceRepository paymentServiceRepository, IMapper mapper ) { 
            _paymentServiceRepository = paymentServiceRepository;
            _mapper = mapper;
        }

        public async Task CreatePaymentAsync(Guid orderId, string customerId, int cartId, decimal total)
        {
            var paymentDetails = new PaymentDetail
            {
                Id = Guid.NewGuid().ToString(),
                TransactionId = Guid.NewGuid().ToString(),
                Email = "customerId@gmail.com",
                CartId = cartId,
                Total = total,
                Status = "Success",
                CreatedDate = DateTime.UtcNow,
                Tax = total * 0.1m, // Assuming a tax rate of 10%   
                Currency = "USD",
                GrandTotal = total + (total * 0.1m),
                UserId = 1 // Assuming customerId can be parsed to an integer
            };
            await _paymentServiceRepository.CreatePaymentAsync(paymentDetails);
        }

        public async Task<GetPaymentResponse> GetPaymentByIdAsync(string paymentId)
        {
            var payment = await _paymentServiceRepository.GetPaymentByIdAsync(paymentId);
            var getPaymentResponse = _mapper.Map<GetPaymentResponse>(payment);
            return getPaymentResponse;
        }

        public async Task UpdatePaymentAsync(UpdatePaymentRequest request)
        {
            var paymentDtls = _mapper.Map<PaymentDetail>(request);
            await _paymentServiceRepository.UpdatePaymentAsync(paymentDtls);
        }
    }
}
