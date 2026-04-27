namespace OrderService.Application.DTOs
{
    public class CreateOrderRequest
    {
        public string PaymentId { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Locality { get; set; }

        public string PhoneNumber { get; set; }

        public long UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? AcceptDate { get; set; }

        public virtual ICollection<CreateOrderItemDto> OrderItems { get; set; } = [];
    }
}
