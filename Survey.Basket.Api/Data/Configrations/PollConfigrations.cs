using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Basket.Api.Data.Entites;

namespace Survey.Basket.Api.Data.Configrations
{
    public class PollConfigrations : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            throw new NotImplementedException();
        }
    }
}
