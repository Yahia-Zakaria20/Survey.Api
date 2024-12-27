using Microsoft.AspNetCore.Mvc;
using Survey.Basket.Api.Error;

namespace Survey.Basket.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next , ILogger<ExceptionHandlerMiddleware> logger)
        {
          _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext httpContext) 
        {
            try
            {
                //request
                
                await _next(httpContext);

                // Response 

            }
            catch ( Exception  ex)
            {
                _logger.LogError(string.Empty, ex.Message);

                var Response = new ApiResponse(StatusCodes.Status500InternalServerError);

               httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
