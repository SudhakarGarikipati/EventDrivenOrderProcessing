using System.Diagnostics;

namespace Shared.Observability
{
    public class Telemetry
    {
        public static readonly ActivitySource OrderActivity = new("OrderService");
        // For PaymentService, change to "PaymentService"

        public static readonly ActivitySource PaymentActivity = new("PaymentService");

    }
}
