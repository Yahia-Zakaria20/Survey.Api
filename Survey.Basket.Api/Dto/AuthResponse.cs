namespace Survey.Basket.Api.Dto
{
    public class AuthResponse
    {
        public string ID { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string Token { get; set; } = string.Empty;

        public int Expirein { get; set; }
    }
}
