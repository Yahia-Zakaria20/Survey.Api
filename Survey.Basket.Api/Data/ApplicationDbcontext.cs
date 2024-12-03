using Microsoft.EntityFrameworkCore;
using Survey.Basket.Api.Data.Entites;

namespace Survey.Basket.Api.Data
{
    public class ApplicationDbcontext:DbContext
    {

        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> optionsBuilder):base(optionsBuilder)
        {
            
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new PollConfigrations());
        //}

        public DbSet<Poll> Polls { get; set; }
    }
}
