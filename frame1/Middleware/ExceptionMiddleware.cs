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
            catch (Exception ex)
            {
                var requestId = context.Items["RequestId"]?.ToString() ?? "";

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Code = "ОШИБКА",
                    Message = $"Произошла ошибка при обработке запроса: {ex.Message}",
                    RequestId = requestId
                };

                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
