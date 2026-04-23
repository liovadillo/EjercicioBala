using System.Diagnostics;

namespace EjercicioBala.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next) {  _next = next; }

        public async Task Invoke(HttpContext context)
        {
            // 1. Imprime el método HTTP y la ruta antes de pasar al siguiente
            // ejemplo: "Request: GET /api/patient"
            var start = DateTime.UtcNow;
            var requestInfo = context.Request;
            Debug.WriteLine($"Request:{requestInfo.Method} {requestInfo.Path}");

            await _next(context);

            // 2. Imprime el status code de la respuesta después de que regresa
            // ejemplo: "Response: 200"
            var responseInfo = context.Response;
            Debug.WriteLine($"Response: {responseInfo.StatusCode} | {(DateTime.UtcNow - start).TotalMilliseconds}ms");
        }
    }
}
