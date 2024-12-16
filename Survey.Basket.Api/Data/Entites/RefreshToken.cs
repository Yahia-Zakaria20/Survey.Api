using Microsoft.EntityFrameworkCore;

namespace Survey.Basket.Api.Data.Entites
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime ExpiresOn { get; set; }

        public DateTime? RevocedOn {  get; set; }

        public bool IsExpired  => DateTime.UtcNow >= ExpiresOn;

        public bool  IsActive  => RevocedOn is null && !IsExpired;
    }
}
