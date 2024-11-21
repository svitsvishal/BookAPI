using System.Net;
using System.Text.Json;

namespace BookAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound, // 404
                ArgumentException => HttpStatusCode.BadRequest,  // 400
                UnauthorizedAccessException => HttpStatusCode.Unauthorized, // 401
                _ => HttpStatusCode.InternalServerError,         // 500
            };

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = exception.Message,
                Timestamp = DateTime.UtcNow
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            // Optional: Log the exception (e.g., using ILogger)
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
