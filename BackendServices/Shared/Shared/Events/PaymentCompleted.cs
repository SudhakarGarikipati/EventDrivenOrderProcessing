namespace Shared.Events
{
    public class PaymentCompleted
    {
        public Guid OrderId { get; set; }
        public string PaymentId { get; set; }
        public DateTime ProcessedAt { get; set; }

    }
}
