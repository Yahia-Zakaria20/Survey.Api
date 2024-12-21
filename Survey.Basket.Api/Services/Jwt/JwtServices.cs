using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Survey.Basket.Api.Services.Jwt
{
    public class JwtServices : IJwtServices
    {
        private readonly JwtOptions _jwtoptions;

        public JwtServices(IOptions<JwtOptions> jwtoptions)
        {
            _jwtoptions = jwtoptions.Value;
        }
        public (string Token, int Expirein) GenerateToKen(ApplicationUser user)
        {
            //RegisterClaims And Roles to know what his access in App
            var Clamis = new List<Claim>() 
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Sub , user.Id),// this User id 
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()), // what is it JTI ? IS A Unique Id For Token 
            };
            // add Roles to get it  From  UserManager 

            //Cohice the Key 

            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtoptions.SecurityKey));

            

            var TokenObj = new JwtSecurityToken
                (
                          //Register Claims
                          audience: _jwtoptions.audience,
                          issuer: _jwtoptions.issuer,
                          expires: DateTime.UtcNow.AddMinutes(_jwtoptions.expires),
                          claims: Clamis,
                          signingCredentials: new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
                );


            return (Token: new JwtSecurityTokenHandler().WriteToken(TokenObj) , Expirein: _jwtoptions.expires * 60);

            
        }

        public string? ValidateJwtToken(string Token)
        {
            var TokenHandler = new JwtSecurityTokenHandler(); 
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtoptions.SecurityKey)); // Becouse ValidatJwtToken input it the same one generated the Firstone

            try
            {
                TokenHandler.ValidateToken(Token, new TokenValidationParameters()
                {
                    IssuerSigningKey = SecurityKey,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // Becouse not watting 5 min after the token Expired
                }, out SecurityToken validatedToken);

                var JwtToken = (JwtSecurityToken)validatedToken;

                return JwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value; // first Bcouse Not to be null 'claims '

            }
            catch 
            {
              return null;
            }



        }
    }
}
