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
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()), // what is it JTI
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
    }
}
