using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CPCommon.Data.Configurations;

internal class ParkingConfiguration : IEntityTypeConfiguration<Parking>
{
	public void Configure(EntityTypeBuilder<Parking> builder)
	{
		builder.HasOne((Parking p) => p.Airport).WithMany((Airport a) => a.Parking).HasForeignKey((Parking p) => p.AirportId);
	}
}
