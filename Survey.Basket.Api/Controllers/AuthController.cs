using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Helper;
using Survey.Basket.Api.Servises.Auth;

namespace Survey.Basket.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly JwtOptions jwtOptions;

        public AuthController(IAuthServices authServices
            ,IOptions<JwtOptions> JwtOptions)
        {
            _authServices = authServices;
            jwtOptions = JwtOptions.Value;
        }

        [HttpPost]
        public async Task<ActionResult<AuthResponse>> Login(LoginDto loginDto,CancellationToken cancellation) 
        {
          var Result =await   _authServices.LoginAsync(loginDto, cancellation);

            return Result is null ?BadRequest() : Ok(Result);
        }
    
     
    }
}
