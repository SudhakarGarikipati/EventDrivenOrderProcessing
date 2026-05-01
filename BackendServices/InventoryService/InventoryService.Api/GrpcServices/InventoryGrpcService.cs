using Azure.Core;
using Grpc.Core;
using InventoryService.Application.DTOs;
using InventoryService.Application.Service.Abstraction;
using InventoryService.Grpc;


namespace InventoryService.Api.GrpcServices
{
    public class InventoryGrpcService : InventoryGrpc.InventoryGrpcBase
    {
        private readonly IInventoryAppService _inventoryAppService;

        public InventoryGrpcService(IInventoryAppService inventoryAppService)
        {
            _inventoryAppService = inventoryAppService;
        }

        public override async Task<CheckStockResponse> CheckStock(CheckStockRequest checkStockRequest, ServerCallContext context)
        {

            var inputDict = checkStockRequest.KeyValuePairs.ToDictionary(k =>Convert.ToInt32(k.Key), v => v.Value);

            var productsAvailable = await _inventoryAppService.CheckStockAvailability(inputDict);
            var response = new CheckStockResponse
            {
                AllAvailable = productsAvailable.All(x => x.Value)
            };

            response.Availability.Add(productsAvailable.ToDictionary(k => k.Key.ToString(), v => v.Value));

            return response;
        }

    }
}
