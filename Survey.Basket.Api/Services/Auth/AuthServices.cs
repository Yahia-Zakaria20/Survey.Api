using Microsoft.AspNetCore.Identity;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Error;
using Survey.Basket.Api.Errors;
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


        public async Task<Result<AuthResponse>> LoginAsync(string Email, string Password, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
                return Result<AuthResponse>.Falier(new ApiResponse(400));

            var IsValidPassword = await _userManager.CheckPasswordAsync(user, Password);
            if (!IsValidPassword)
                return Result<AuthResponse>.Falier(new ApiResponse(400));

            var (Token, ExpireDate) = _jwtServices.GenerateToKen(user);

            var _RefreshToken = GetRefreshToken();

            var _RefreshTokenExpiration = DateTime.UtcNow.AddDays(_reftokenexpiration);

            user.RefreshTokens.Add(new RefreshToken()
            {
                Token = _RefreshToken,
                ExpiresOn = _RefreshTokenExpiration,
            });

            await  _userManager.UpdateAsync(user);

            return Result<AuthResponse>.Success(new AuthResponse() { Email = user.Email, FirstName = user.FirstName, ID = user.Id, LastName = user.LastName, Token = Token, Expirein = ExpireDate, RefreshToke = _RefreshToken, RefreshTokenExpiration = _RefreshTokenExpiration });

            
        }

        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellation)
        {
          // chaeck for structure of token from isvalidtokenfunc and get user id from it 

            var userid = _jwtServices.ValidateJwtToken(Token);
            if (userid is null)
                return Result<AuthResponse>.Falier(new ApiResponse(400));

           var user = await _userManager.FindByIdAsync(userid);
            if (user is null) 
                return Result<AuthResponse>.Falier(new ApiResponse(401));

            var RefToken = user.RefreshTokens.SingleOrDefault(r => r.Token == RefreshToken && r.IsActive);
            if(RefToken is null)
                return Result<AuthResponse>.Falier(new ApiResponse(401));

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
            return Result<AuthResponse>.Success(new AuthResponse() { Email = user.Email, FirstName = user.FirstName, ID = user.Id, LastName = user.LastName, Token = NewToken, Expirein = ExpireDate, RefreshToke = _NewRefreshToken, RefreshTokenExpiration = _NewRefreshTokenExpiration });
        }

        public async Task<Result> RevocedRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellation)
        {
            var userid = _jwtServices.ValidateJwtToken(Token);
            if (userid is null)
                return Result.Falier(new ApiResponse(400));

            var user = await _userManager.FindByIdAsync(userid);
            if (user is null)
                return Result.Falier(new ApiResponse(400));

            var RefToken = user.RefreshTokens.SingleOrDefault(r => r.Token == RefreshToken && r.IsActive);
            if (RefToken is null)
                return Result.Falier(new ApiResponse(400));

            RefToken.RevocedOn = DateTime.UtcNow;


           await   _userManager.UpdateAsync(user);

            return Result.Success();

        }
        private static string GetRefreshToken()
        {
          
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
           
        }

       
    }
}
