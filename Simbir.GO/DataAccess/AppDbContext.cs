using Microsoft.EntityFrameworkCore;
using Simbir.GO.DataAccess.Objects;

namespace Simbir.GO.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> Tokens { get; set; }

        //public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder
        //         //.UseNpgsql(new ConfigurationManager().GetConnectionString("default"))
        //         .UseSnakeCaseNamingConvention();
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();
        }
    }
}