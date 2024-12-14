using Microsoft.AspNetCore.Identity;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Dto;

namespace Survey.Basket.Api.Servises.Auth
{
    public interface IAuthServices
    {
        public Task<AuthResponse?> LoginAsync(LoginDto loginDto, CancellationToken cancellation);
    }
}
