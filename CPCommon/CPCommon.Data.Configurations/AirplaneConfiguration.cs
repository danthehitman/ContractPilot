using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CPCommon.Data.Configurations;

internal class AirplaneConfiguration : IEntityTypeConfiguration<Airplane>
{
	public void Configure(EntityTypeBuilder<Airplane> builder)
	{
	}
}
