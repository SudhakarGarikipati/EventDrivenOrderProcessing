using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OrderService.Infrastructure.Observability.Correlation
{
    public class CorrelationIdMiddleware
    {
        private const string Header = "X-Correlation-Id";
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<CorrelationIdMiddleware> logger)
        {
            if (!context.Request.Headers.TryGetValue(Header, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers.Append(Header, correlationId);
            }

            context.Response.Headers[Header] = correlationId;

            using (logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId.ToString()
            }))
            {
                await _next(context);
            }
        }

    }
}
