namespace Shared.Events
{
    public class OrderCreated
    {
        public Guid OrderId { get; set; }

        public int CartId { get; set; }

        public string CustomerId { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
