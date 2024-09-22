namespace APIGateway
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

        public async Task InvokeAsync(HttpContext context)
        {
            var requestBody = await FormatRequest(context.Request);
            _logger.LogInformation("Request: {method} {url} {body}", context.Request.Method, context.Request.Path, requestBody);

            await _next(context);

            var responseBody = context.Response.StatusCode;
            _logger.LogInformation("Response: {statusCode}", responseBody);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }
    }

}
