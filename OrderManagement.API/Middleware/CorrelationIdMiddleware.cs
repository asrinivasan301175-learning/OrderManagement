using Serilog.Context;

namespace OrderManagement.API.Middleware;

public sealed class CorrelationIdMiddleware
{
    private const string HeaderName = "X-Correlation-ID";
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var incomingId = context.Request.Headers[HeaderName]
            .FirstOrDefault();

        // Accept only a valid GUID. Otherwise create a new ID.
        var correlationId =
            Guid.TryParse(incomingId, out var parsedId)
                ? parsedId.ToString()
                : Guid.NewGuid().ToString();

        context.Response.Headers[HeaderName] = correlationId;

        // Add the ID to every Serilog event in this request scope.
        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await _next(context);
        }
    }
}