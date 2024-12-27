using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Error;
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
        public async Task<ActionResult<AuthResponse>> LoginAsync([FromBody]LoginDto loginDto,CancellationToken cancellation) 
        {
          var Result =await   _authServices.LoginAsync(loginDto.Email,loginDto.Password, cancellation);

            return Result is null ?BadRequest(new ApiResponse(StatusCodes.Status400BadRequest)) : Ok(Result);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>> RefreshTokenAsync([FromBody]RefreshTokenDto dto, CancellationToken cancellation)
        {
            var Result = await _authServices.GetRefreshTokenAsync(dto.Token,dto.RefreshToken, cancellation);

            return Result is null ? BadRequest(new ApiResponse(StatusCodes.Status400BadRequest)) : Ok(Result);
        }

        [HttpPost("revoced-refresh-token")]
        public async Task<ActionResult> RevocedRefreshTokenAsync([FromBody] RefreshTokenDto dto, CancellationToken cancellation)
        {
            var Result = await _authServices.RevocedRefreshTokenAsync(dto.Token, dto.RefreshToken, cancellation);

            return Result is false ? BadRequest(new ApiResponse(StatusCodes.Status400BadRequest)) : Ok();
        }


    }
}
