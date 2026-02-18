using frame1.Models;

namespace frame1.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                await WriteError(context, "NOT_FOUND", ex.Message, 404);
            }
            catch (ArgumentException ex)
            {
                await WriteError(context, "VALIDATION_ERROR", ex.Message, 400);
            }
            catch (Exception)
            {
                await WriteError(
                    context,
                    "INTERNAL_ERROR",
                    "Произошла внутренняя ошибка сервера",
                    500
                );
            }
        }

        private async Task WriteError(
            HttpContext context,
            string code,
            string message,
            int statusCode)
        {
            var requestId = context.Items["RequestId"]?.ToString() ?? "";

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var error = new ErrorResponse
            {
                Code = code,
                Message = message,
                RequestId = requestId
            };

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
