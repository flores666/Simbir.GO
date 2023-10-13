using Microsoft.EntityFrameworkCore;
using Simbir.GO.DataAccess.Objects;

namespace Simbir.GO.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> Tokens { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}