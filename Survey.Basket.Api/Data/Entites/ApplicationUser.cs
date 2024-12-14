using Microsoft.AspNetCore.Identity;

namespace Survey.Basket.Api.Data.Entites
{
    public class ApplicationUser:IdentityUser
    {

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
