using System.Net;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                 await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _environment.IsDevelopment() 
                    ? new ApiException((int)HttpStatusCode.InternalServerError, exception.Message, exception.StackTrace) 
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                //var jsonResponse = JsonSerializer.Serialize(response);
                //await context.Response.WriteAsync(jsonResponse);

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}