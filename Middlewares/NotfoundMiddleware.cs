
#pragma warning disable 1591
namespace webapi.Middlewares
{
    public class NotfoundMiddleware
    {
        private RequestDelegate _next;
        public NotfoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILogger<NotfoundMiddleware> logger)
        {
            await _next(context);
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                logger.LogInformation("Not found");
                context.Response.StatusCode = StatusCodes.Status200OK;
                await context.Response.WriteAsJsonAsync(new { hello = "world" });
            }
        }
    }

    public static class NotfoundMiddlewareExtensions
    {
        public static IApplicationBuilder UseNotfoundMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotfoundMiddleware>();
        }
    }
}