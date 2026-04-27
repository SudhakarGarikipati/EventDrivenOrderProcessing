using PaymentService.Application.DTOs;

namespace PaymentService.Application.Service.Abstraction
{
    public interface IPaymentAppService
    {
        public Task CreatePaymentAsync(CreatePaymentRequest request);
        public Task UpdatePaymentAsync(UpdatePaymentRequest request);
        public Task<GetPaymentResponse> GetPaymentByIdAsync(string paymentId);
    }
}
