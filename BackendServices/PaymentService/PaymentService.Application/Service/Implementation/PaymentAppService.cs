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

        public async Task CreatePaymentAsync(CreatePaymentRequest request)
        {
            var paymentDtls = _mapper.Map<PaymentDetail>(request);
            await _paymentServiceRepository.CreatePaymentAsync(paymentDtls);
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
