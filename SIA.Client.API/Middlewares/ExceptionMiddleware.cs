using System.Net;
using System.Text.Json;

namespace SIA.Client.API.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context.Response.Headers.Server = "no-server";
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"error during executing {context}", context.Request.Path.Value);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new { message = ex.Message, stackTrack = ex.StackTrace, context.Response.StatusCode }));

            /*
            if (!ex.Message.Contains("An item with the same key has already been added"))
                return context.Response.WriteAsync(JsonSerializer.Serialize(new { message = ex.Message, stackTrack = ex.StackTrace, context.Response.StatusCode }));
            else
                return context.Response.WriteAsync(JsonSerializer.Serialize(new { message = "Oops! Something went wrong. Try again please!", stackTrack = ex.StackTrace, context.Response.StatusCode }));
            */
        }
    }

    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
