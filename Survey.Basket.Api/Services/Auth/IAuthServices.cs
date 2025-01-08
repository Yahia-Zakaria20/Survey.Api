using Microsoft.AspNetCore.Identity;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;
using Survey.Basket.Api.Errors;

namespace Survey.Basket.Api.Servises.Auth
{
    public interface IAuthServices
    {
        public Task<Result<AuthResponse>> LoginAsync(string Email, string Password, CancellationToken cancellation);

        public Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token,string RefreshToken, CancellationToken cancellation);
        public Task<Result> RevocedRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellation);
    }
}
