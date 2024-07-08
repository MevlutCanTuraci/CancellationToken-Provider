namespace App.CancellationTokenApi.Infrastructer.Middlewares;

public class CancellationTokenMiddleware
{
    private readonly RequestDelegate _next;

    public CancellationTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Add CancellationToken to HttpContext.Items
        context.Items["CancellationToken"] = context.RequestAborted;
        await _next(context);
    }
}