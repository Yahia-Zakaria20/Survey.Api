using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Basket.Api.Data.Entites;

namespace Survey.Basket.Api.Data.Configrations
{
    public class PollConfigrations : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> Entite)
        {
            Entite.HasIndex(p => p.Titel)
                .IsUnique();

            Entite.Property(p => p.Titel)
                .HasMaxLength(100);

               Entite.Property(p => p.Summary)
                .HasMaxLength(1500);
              
        }
    }
}
