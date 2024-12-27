using Microsoft.AspNetCore.Diagnostics;
using Survey.Basket.Api.Error;

namespace Survey.Basket.Api.Errors
{
    public class GlobalExeptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExeptionHandler> _logger;

        public GlobalExeptionHandler(ILogger<GlobalExeptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(string.Empty, exception.Message);

            var Response = new ApiResponse(StatusCodes.Status500InternalServerError);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

           await httpContext.Response.WriteAsJsonAsync(Response,cancellationToken);


            return true;
        }
    }
}
