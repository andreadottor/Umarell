namespace Dottor.Umarell.Server.Models
{
    using Microsoft.EntityFrameworkCore;

    public class UmarellContext : DbContext
    {

        public DbSet<BuildingSite> BuildingSites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuildingSite>().ToTable("BuildingSites");
        }
    }
}
