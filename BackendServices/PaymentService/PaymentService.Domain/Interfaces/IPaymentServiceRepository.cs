using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Interfaces
{
    public interface IPaymentServiceRepository
    {
        public Task CreatePaymentAsync(PaymentDetail payment);
        public Task UpdatePaymentAsync(PaymentDetail payment);
        public Task<PaymentDetail> GetPaymentByIdAsync(string paymentId);
    }
}
