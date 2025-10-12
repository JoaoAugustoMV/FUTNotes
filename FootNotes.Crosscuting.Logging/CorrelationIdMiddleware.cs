using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace FootNotes.Crosscuting.Logging
{
    public class CorrelationIdMiddleware(RequestDelegate next)
    {
        private const string CorrelationHeader = "X-Correlation-Id";

        public async Task InvokeAsync(HttpContext context)
        {
            string correlationId = context.Request.Headers.TryGetValue(CorrelationHeader, out StringValues value)
                ? value.ToString()
                : Guid.NewGuid().ToString();

            
            context.Response.Headers[CorrelationHeader] = correlationId;
            LogContext.PushProperty("CorrelationId", correlationId);

            await next(context);
        }
    }
}
