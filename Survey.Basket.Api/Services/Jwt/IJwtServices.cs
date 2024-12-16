using Survey.Basket.Api.Data.Entites;

namespace Survey.Basket.Api.Services.Jwt
{
    public interface IJwtServices
    {
        (string Token, int Expirein) GenerateToKen(ApplicationUser user);

       public string? ValidateJwtToken(string JwtToken);
    }
}
