namespace PaymentService.Application.DTOs
{
    public class CreatePaymentRequest
    {

        public string TransactionId { get; set; }

        public decimal Tax { get; set; }

        public string Currency { get; set; }

        public decimal Total { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public int CartId { get; set; }

        public decimal GrandTotal { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }
    }
}
