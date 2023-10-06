using Lnm2CpLoader.Model;
using Microsoft.EntityFrameworkCore;

namespace Lnm2CpLoader.Data;
public class LnmAirportContext : DbContext
{
    public DbSet<LnmAirportModel> Airports { get; set; }

    public DbSet<LnmParkingModel> Parking { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=D:\\Dan\\Dev\\ContractPilot\\Data\\little_navmap_msfs.sqlite");
    }
}
