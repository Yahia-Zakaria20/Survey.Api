using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Survey.Basket.Api.Data.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace Survey.Basket.Api.Data
{
    public class ApplicationDbcontext:IdentityDbContext<IdentityUser>
    {
        private readonly IHttpContextAccessor _httpContext;

        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> optionsBuilder,
            IHttpContextAccessor httpContext):base(optionsBuilder)
        {
           _httpContext = httpContext;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Poll> Polls { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var Entites = ChangeTracker.Entries<AuditableEntite>(); //this Var has A lot Of Entite for Any Calss Inhrtance From AuitableEntite

            var UserId = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            foreach (var entite in Entites) 
            {
                if (entite.State == EntityState.Added) 
                {
                    entite.Property(e => e.CreatedById).CurrentValue = UserId;
                }
                else if(entite.State == EntityState.Modified) 
                {
                    entite.Property(e => e.UpdetedById).CurrentValue = UserId;
                    entite.Property(e => e.UpdetedOn).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
