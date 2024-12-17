namespace Survey.Basket.Api.Data.Entites
{
    public class AuditableEntite
    {
      
        public string CreatedById { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ApplicationUser CreatedBy { get; set; } = default!;

        public string? UpdetedById { get; set; }

        public DateTime? UpdetedOn { get; set; } 

        public ApplicationUser UpdetedBy { get; set; } = default!;
    }
}
