using InventoryService.Grpc;
using OrderService.Application.Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.gRPC
{
    public class InventoryServiceClient : IInventoryService
    {
        private readonly InventoryGrpc.InventoryGrpcClient _client;

        public InventoryServiceClient(InventoryGrpc.InventoryGrpcClient client)
        {
            _client = client;
        }

        public async Task<Dictionary<string, bool>> CheckStockAsync(Dictionary<string, int> products)
        {
            var request = new CheckStockRequest();
            request.KeyValuePairs.Add(products);

            var response = await _client.CheckStockAsync(request);

            return response.Availability.ToDictionary();
        }
    }
}
