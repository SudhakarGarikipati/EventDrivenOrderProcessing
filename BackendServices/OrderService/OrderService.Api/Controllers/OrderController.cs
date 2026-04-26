using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.Service.Abstraction;

namespace PaymentService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Order([FromBody] CreateOrderRequest createOrderRequest)
        {
            if (createOrderRequest == null)
            {
                return NoContent();
            }
            await _orderAppService.CreateOrderAsync(createOrderRequest);
            return Ok("Order created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Order([FromBody] UpdateOrderRequest updateOrderRequest)
        {
            if(updateOrderRequest == null)
            {
                return NoContent();
            }
            await _orderAppService.UpdateOrderAsync(updateOrderRequest);
            return Ok("Order updated successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Order(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid order ID.");
            }
            var order = await _orderAppService.GetOrderByIdAsync(Guid.Parse(id.ToString()));
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
