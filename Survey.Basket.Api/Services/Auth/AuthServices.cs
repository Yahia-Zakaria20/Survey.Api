using Microsoft.AspNetCore.Identity;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Services.Jwt;
using Survey.Basket.Api.Servises.Auth;
using System.Security.Cryptography;

namespace Survey.Basket.Api.Services.Auth
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtServices _jwtServices;

        private readonly int _reftokenexpiration = 14;
        public AuthServices(UserManager<ApplicationUser> userManager
            ,IJwtServices jwtServices) 
        {
            _userManager = userManager;
            _jwtServices = jwtServices;
        }


        public async Task<AuthResponse?> LoginAsync(string Email, string Password, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
                return null;

            var IsValidPassword = await _userManager.CheckPasswordAsync(user, Password);
            if (!IsValidPassword)
                return null;

            var (Token, ExpireDate) = _jwtServices.GenerateToKen(user);

            var _RefreshToken = GetRefreshToken();

            var _RefreshTokenExpiration = DateTime.UtcNow.AddDays(_reftokenexpiration);

            user.RefreshTokens.Add(new RefreshToken()
            {
                Token = _RefreshToken,
                ExpiresOn = _RefreshTokenExpiration,
            });

            await  _userManager.UpdateAsync(user);
            return new AuthResponse() { Email = user.Email, FirstName = user.FirstName, ID = user.Id, LastName = user.LastName, Token = Token, Expirein = ExpireDate, RefreshToke = _RefreshToken , RefreshTokenExpiration = _RefreshTokenExpiration };

            
        }

        public async Task<AuthResponse?> GetRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellation)
        {
          // chaeck for structure of token from isvalidtokenfunc and get user id from it 

            var userid = _jwtServices.ValidateJwtToken(Token);
            if (userid is null)
                return null;

           var user = await _userManager.FindByIdAsync(userid);
            if (user is null) 
                return null;

            var RefToken = user.RefreshTokens.SingleOrDefault(r => r.Token == RefreshToken && r.IsActive);
            if(RefToken is null)
                return null;

             RefToken.RevocedOn = DateTime.UtcNow;

            // and get user from data base with refreshtoken and filter by refreshtoken and must be isActive 


            var (NewToken, ExpireDate) = _jwtServices.GenerateToKen(user);

            var _NewRefreshToken = GetRefreshToken();

            var _NewRefreshTokenExpiration = DateTime.UtcNow.AddDays(_reftokenexpiration);

            user.RefreshTokens.Add(new RefreshToken()
            {
                Token = _NewRefreshToken,
                ExpiresOn = _NewRefreshTokenExpiration,
            });

            await _userManager.UpdateAsync(user);
            return new AuthResponse() { Email = user.Email, FirstName = user.FirstName, ID = user.Id, LastName = user.LastName, Token = NewToken, Expirein = ExpireDate, RefreshToke = _NewRefreshToken, RefreshTokenExpiration = _NewRefreshTokenExpiration };
        }

        public async Task<bool> RevocedRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellation)
        {
            var userid = _jwtServices.ValidateJwtToken(Token);
            if (userid is null)
                return false;

            var user = await _userManager.FindByIdAsync(userid);
            if (user is null)
                return false;

            var RefToken = user.RefreshTokens.SingleOrDefault(r => r.Token == RefreshToken && r.IsActive);
            if (RefToken is null)
                return false;

            RefToken.RevocedOn = DateTime.UtcNow;


           await   _userManager.UpdateAsync(user);

            return true;
        }
        private static string GetRefreshToken()
        {
          
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
           
        }

       
    }
}
