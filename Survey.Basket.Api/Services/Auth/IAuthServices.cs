using Microsoft.AspNetCore.Identity;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;

namespace Survey.Basket.Api.Servises.Auth
{
    public interface IAuthServices
    {
        public Task<AuthResponse?> LoginAsync(string Email, string Password, CancellationToken cancellation);

        public Task<AuthResponse?> GetRefreshTokenAsync(string Token,string RefreshToken, CancellationToken cancellation);
        public Task<bool> RevocedRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellation);
    }
}
