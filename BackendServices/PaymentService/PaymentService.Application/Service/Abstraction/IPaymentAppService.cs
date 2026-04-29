using PaymentService.Application.DTOs;

namespace PaymentService.Application.Service.Abstraction
{
    public interface IPaymentAppService
    {
        public Task CreatePaymentAsync(Guid orderId, string customerId, int cartId, decimal total);
        public Task UpdatePaymentAsync(UpdatePaymentRequest request);
        public Task<GetPaymentResponse> GetPaymentByIdAsync(string paymentId);
    }
}
