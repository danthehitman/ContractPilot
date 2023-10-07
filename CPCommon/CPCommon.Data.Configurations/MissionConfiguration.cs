using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CPCommon.Data.Configurations;

internal class MissionConfiguration : IEntityTypeConfiguration<Mission>
{
	public void Configure(EntityTypeBuilder<Mission> builder)
	{
		builder.Property((Mission m) => m.Cargo).HasColumnType("jsonb");
		builder.Property((Mission m) => m.Passengers).HasColumnType("jsonb");
	}
}
