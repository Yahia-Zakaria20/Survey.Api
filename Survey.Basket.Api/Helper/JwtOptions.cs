using System.ComponentModel.DataAnnotations;

namespace Survey.Basket.Api.Helper
{
    public class JwtOptions
    {
        public static string SectionName = "Jwt";
        [Required]
        public string SecurityKey { get;init; } =string.Empty;
        [Required]
        public string issuer { get; init; } =string.Empty;
        [Required]
        public string audience { get;init; } =string.Empty;
        [Range(1,int.MaxValue),Required]
        public int expires { get;init;}
    }
}
