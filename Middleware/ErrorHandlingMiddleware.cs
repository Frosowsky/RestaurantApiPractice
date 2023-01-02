using WebApplication3.Exceptions;

namespace WebApplication3.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public ILogger<ErrorHandlingMiddleware> Logger { get; }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
              await next.Invoke(context);
            }
            catch(NotFoundException notFoundExceptions)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundExceptions.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
               await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
