using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survey.Basket.Api.Error;


namespace Survey.Basket.Api.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {

        public ActionResult geterror(int code) 
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
