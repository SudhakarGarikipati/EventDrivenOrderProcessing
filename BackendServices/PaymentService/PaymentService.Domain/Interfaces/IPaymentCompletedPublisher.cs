using Shared.Events;

namespace PaymentService.Domain.Interfaces
{
    public interface IPaymentCompletedPublisher
    {
       Task PublishAsync(PaymentCompleted paymentCompleted);
    }
}
