using IO.Swagger.DataAccess.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IO.Swagger.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("default"))
                .UseSnakeCaseNamingConvention();
        }
    }
}