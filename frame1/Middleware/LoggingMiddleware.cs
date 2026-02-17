namespace frame1.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestId = Guid.NewGuid().ToString();
            context.Items["RequestId"] = requestId;

            _logger.LogInformation(
                $"Запрос {requestId} начат. Метод: {context.Request.Method}, Путь: {context.Request.Path}"
            );

            await _next(context);

            _logger.LogInformation(
                $"Запрос {requestId} завершён. Код ответа: {context.Response.StatusCode}"
            );
        }
    }
}
