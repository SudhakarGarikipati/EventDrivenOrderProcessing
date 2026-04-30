using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Observability
{
    public class Telemetry
    {
        public static readonly ActivitySource OrderActivity = new("OrderService");
        // For PaymentService, change to "PaymentService"

        public static readonly ActivitySource PaymentActivity = new("PaymentService");

    }
}
