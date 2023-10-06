using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace CPCommon.Data;

public class CPContext : DbContext
{
    public DbSet<Airport> Airports { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserAirport> UserAirports { get; set; }

    public DbSet<Parking> Parking { get; set; }

    public DbSet<Airplane> Airplanes { get; set; }

    public DbSet<UserAirplane> UserAirplanes { get; set; }

    public DbSet<AirplaneMakeModelInfo> AirplaneMakeModelInfos { get; set; }

    public DbSet<Mission> Missions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=localhost;Database=cpdata;Username=postgres;Password=postgres");
        dataSourceBuilder.UseNetTopologySuite();
        var dataSource = dataSourceBuilder.Build();

        optionsBuilder.UseNpgsql("Host=localhost;Database=cpdata;Username=postgres;Password=postgres", delegate (NpgsqlDbContextOptionsBuilder o)
        {
            o.UseNetTopologySuite();
        });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("postgis");
        builder.ApplyConfigurationsFromAssembly(typeof(CPContext).Assembly);
        builder.HasPostgresEnum<EngineType>();
    }
}
