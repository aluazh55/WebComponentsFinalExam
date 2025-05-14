using System.Diagnostics;

namespace Villa.RealEstate.AppMiddleware
{
    public class UseTimeElapsed
    {
        private readonly ILogger<UseTimeElapsed> _logger;
        private readonly RequestDelegate _next;

        public UseTimeElapsed(RequestDelegate next,
            ILogger<UseTimeElapsed> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var timer = Stopwatch.StartNew();
            await _next(context);

            timer.Stop();
            var elapsedMs = timer.Elapsed.TotalMilliseconds;
            string page = context.Request.Path;

            string path = context.Request.Path;
            string method = context.Request.Method;
            string user = context.User.Identity != null ?
                context.User.Identity.Name : "Guest";
            _logger.LogWarning("Регистрация запроса от пользователя {user}, по пути " + "{path},метода {method}, отработано за {elapsedMs}",
                user, path, elapsedMs);


        }

    }
}
