using Microsoft.AspNetCore.Identity;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Services.Jwt;
using Survey.Basket.Api.Servises.Auth;

namespace Survey.Basket.Api.Services.Auth
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtServices _jwtServices;

        public AuthServices(UserManager<ApplicationUser> userManager
            ,IJwtServices jwtServices) 
        {
            _userManager = userManager;
            _jwtServices = jwtServices;
        }


        public async Task<AuthResponse?> LoginAsync(LoginDto loginDto, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(user is null)
                return null;

          var IsValidPassword =await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!IsValidPassword) 
                return null;

            var (Token, ExpireDate) = _jwtServices.GenerateToKen(user);

            return new AuthResponse() {Email = user.Email , FirstName = user.FirstName , ID = user.Id , LastName = user.LastName , Token  =  Token, Expirein = ExpireDate };


        }
        

       
    }
}
