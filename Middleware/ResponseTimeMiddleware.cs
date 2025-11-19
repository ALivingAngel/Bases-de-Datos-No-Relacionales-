using System.Diagnostics;
using WebApiDatas.Interface;
using WebModel.Metrics;

namespace WebApi.Middleware
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IResponseTimeLogRepository repo)
        {
            var sw = Stopwatch.StartNew();

            await _next(context);

            sw.Stop();

            await repo.AddAsync(new ResponseTimeLog
            {
                Path = context.Request.Path,
                Method = context.Request.Method,
                DurationMs = sw.ElapsedMilliseconds,
                StatusCode = context.Response.StatusCode
            });
        }

    }
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseTimeLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseTimeMiddleware>();
        }
    }

}
