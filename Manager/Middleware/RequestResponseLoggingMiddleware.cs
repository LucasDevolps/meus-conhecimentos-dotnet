using Manager.Infrastructure.Services;
using System.Diagnostics;
using System.Text;

namespace Manager.WebApi.Middleware;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next) => _next = next;


    public async Task InvokeAsync(HttpContext context, ILogRequestResponseService logService)
    {
        var stopwatch = Stopwatch.StartNew();

        string? requestBody = null;
        string? responseBody = null;

        context.Request.EnableBuffering();
        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        var originalBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await _next(context);

        stopwatch.Stop();

        responseBodyStream.Seek(0, SeekOrigin.Begin);
        responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        await responseBodyStream.CopyToAsync(originalBodyStream);

        string? user = context.User?.Identity?.Name ?? "Anonymous";

        await logService.SaveAsync(
            user,
            context.Request.Method,
            context.Request.Path,
            requestBody,
            responseBody,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds
        );
    }
}