using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Basket.Api.Data.Entites;

namespace Survey.Basket.Api.Data.Configrations
{
    public class ApplicationUserConfigrationsigrations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> Entite)
        {
           
            Entite.Property(p => p.FirstName)
                .HasMaxLength(100);

               Entite.Property(p => p.LastName)
                .HasMaxLength(100);   
        }
    }
}
