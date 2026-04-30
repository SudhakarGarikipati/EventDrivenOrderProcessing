using InventoryService.Application.DTOs;
using InventoryService.Application.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {

        private readonly IInventoryAppService _inventoryAppService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IInventoryAppService inventoryAppService) { 
            _logger = logger;
            _inventoryAppService = inventoryAppService;
        }

        [HttpGet]
        public async Task<IActionResult> CheckAvailability([FromBody] CheckStockDTO checkStockDTO)
        {
            _logger.LogInformation("Started CheckAvailability for the selected products.");
            var inputDict = checkStockDTO.keyValuePairs;
            if (inputDict == null || inputDict.Count ==0)
            {
                return BadRequest();
            }
            var res = await _inventoryAppService.CheckStockAvailability(inputDict);
            _logger.LogInformation("Completed CheckAvailability for the selected products.");
            if (res != null && res.Count == inputDict.Count)
            {
                return Ok(res);
            }
            return NoContent();
        }
    }
}
