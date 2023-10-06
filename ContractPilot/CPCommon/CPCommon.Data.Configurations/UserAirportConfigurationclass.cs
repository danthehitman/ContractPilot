using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CPCommon.Data.Configurations;

internal class UserAirportConfigurationclass : IEntityTypeConfiguration<UserAirport>
{
	public void Configure(EntityTypeBuilder<UserAirport> builder)
	{
		builder.HasKey((UserAirport ua) => new { ua.UserId, ua.AirportId });
		builder.HasOne((UserAirport ua) => ua.User).WithMany((User u) => u.UserAirports).HasForeignKey((UserAirport ua) => ua.UserId);
	}
}
