using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CPCommon.Data.Configurations;

internal class AirplaneMakeModelInfoConfiguration : IEntityTypeConfiguration<AirplaneMakeModelInfo>
{
	public void Configure(EntityTypeBuilder<AirplaneMakeModelInfo> builder)
	{
		builder.HasMany((AirplaneMakeModelInfo m) => m.Airplanes).WithOne((Airplane a) => a.MakeModelInfo);
		builder.Property((AirplaneMakeModelInfo m) => m.FuelTanks).HasColumnType("jsonb");
		builder.Property((AirplaneMakeModelInfo m) => m.PayloadStations).HasColumnType("jsonb");
	}
}
