namespace Villa.RealEstate.AppMiddleware
{
    public class UseloggingMiddleware
    {
        private readonly ILogger<UseloggingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public UseloggingMiddleware(RequestDelegate next, ILogger<UseloggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path;
            string method = context.Request.Method;
            string user = context.User.Identity != null ?
                context.User.Identity.Name : "Guest";
            _logger.LogWarning("Регистрация запроса от пользователя {user}, по пути " +
                "{path},метода {method}", user, path, method);
            await _next(context);
        }


    }

    public static class UseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UseloggingMiddleware>();
        }
    }
}
