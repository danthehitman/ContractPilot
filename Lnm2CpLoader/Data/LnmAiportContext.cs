using CPCommon.Model;
using Microsoft.EntityFrameworkCore;

namespace CPCommon.Data
{
    public class LnmAirportContext : DbContext
    {
        public DbSet<LnmAirportModel> Airports { get; set; }

        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=D:\Dan\Dev\ContractPilot\Data\little_navmap_msfs.sqlite");
    }
}
