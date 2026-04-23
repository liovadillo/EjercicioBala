using EjercicioBala.DTO;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace EjercicioBala.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next) { _next = next; }

        public async Task Invoke(HttpContext context)
        {        
            try
            {
                Debug.WriteLine($"Request: {context.Request.Method}:{context.Request.Path} ");
                await _next(context);

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                ErrorResponseDTO errorResponseDTO = new ErrorResponseDTO{ statusCode = 500, message = "Internal Server Error", detail = ex.Message };
                await context.Response.WriteAsJsonAsync(errorResponseDTO);                          
            }
            finally {
                Debug.WriteLine($"Response{(context.Response.StatusCode == 500 ? "_ERROR:":":")} {context.Response.StatusCode}");
            } 
        }
    }
}
