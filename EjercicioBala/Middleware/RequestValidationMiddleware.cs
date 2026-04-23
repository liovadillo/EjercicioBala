namespace EjercicioBala.Middleware
{
    public class RequestValidationMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestValidationMiddleware(RequestDelegate next) { 
            _next = next; 
        }

        public async Task Invoke(HttpContext context)
        {
            if ((context.Request.Method == "POST" || context.Request.Method == "PUT") && !context.Request.Headers.ContentType.Contains("application/json"))
            {
                context.Response.StatusCode = 415;
                await context.Response.WriteAsync("Unsupported Media Type");
                return;
            }

            await _next(context);

        }


    }
}
