using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Service.Abstraction
{
    public interface IInventoryService
    {
        Task<Dictionary<string, bool>> CheckStockAsync(Dictionary<string, int> products);

    }
}
