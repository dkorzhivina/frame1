namespace frame1.Middleware
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimingMiddleware> _logger;

        public TimingMiddleware(RequestDelegate next, ILogger<TimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            var requestId = context.Items["RequestId"]?.ToString();

            _logger.LogInformation(
                $"Запрос {requestId} обработан за {stopwatch.ElapsedMilliseconds} мс"
            );
        }
    }
}
