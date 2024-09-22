namespace APIGateway
{
    public class TokenValidationMiddleware
    {
        public readonly RequestDelegate _next;
        private const string Token = "SuperSecretToken123";

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Authorization header missing");
                return;
            }

            if (!string.Equals(tokenHeader, $"Bearer {Token}", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            await _next(context);
        }
    }
}
