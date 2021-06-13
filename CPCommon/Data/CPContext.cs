using CPCommon.Model;
using Microsoft.EntityFrameworkCore;

namespace CPCommon.Data
{
    public class CPContext : DbContext
    {
        public DbSet<Airport> Airports { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAirport> UserAirports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=cpdata;Username=postgres;Password=postgres",
                o => o.UseNetTopologySuite());
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("postgis");
            builder.ApplyConfigurationsFromAssembly(typeof(CPContext).Assembly);

        }
    }
}
