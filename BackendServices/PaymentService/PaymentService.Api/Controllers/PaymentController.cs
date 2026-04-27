using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.DTOs;
using PaymentService.Application.Service.Abstraction;

namespace PaymentService.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentAppService _paymentAppService;

        public PaymentController(IPaymentAppService paymentAppService)
        {
            _paymentAppService = paymentAppService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> PaymentDetails(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid payment ID.");
            }
            var paymentDetails = await _paymentAppService.GetPaymentByIdAsync(id.ToString());
            if (paymentDetails == null)
            {
                return NotFound($"Payment with ID {id} not found.");
            }
            return Ok(paymentDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Payment([FromBody] CreatePaymentRequest createPaymentRequest)
        {
            if (createPaymentRequest == null)
            {
                return BadRequest("Payment details are required.");
            }
            await _paymentAppService.CreatePaymentAsync(createPaymentRequest);
            return Ok("Payment created successfully.");

        }

        [HttpPut]
        public async Task<IActionResult> Payment([FromBody] UpdatePaymentRequest updatePaymentRequest)
        {
            if (updatePaymentRequest == null)
            {
                return BadRequest("Payment details are required.");
            }
            await _paymentAppService.UpdatePaymentAsync(updatePaymentRequest);
            return Ok("Payment updated successfully.");

        }
    }
}
