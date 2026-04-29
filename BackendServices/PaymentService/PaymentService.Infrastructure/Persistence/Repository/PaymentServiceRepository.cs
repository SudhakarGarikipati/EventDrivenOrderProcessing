using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Infrastructure.Persistence.Repository
{
    public class PaymentServiceRepository : IPaymentServiceRepository
    {
        private readonly PaymentServiceDbContext _context;

        public PaymentServiceRepository(PaymentServiceDbContext context)
        {
            _context = context;
        }

        public async Task CreatePaymentAsync(PaymentDetail payment)
        {
            await _context.PaymentDetails.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<PaymentDetail> GetPaymentByIdAsync(string paymentId)
        {
            var payment = await _context.PaymentDetails.Where(p => p.Id == paymentId).FirstOrDefaultAsync();
            return payment;
        }

        public async Task UpdatePaymentAsync(PaymentDetail payment)
        {
            _context.PaymentDetails.Update(payment); 
                await _context.SaveChangesAsync();
        }
    }
}
