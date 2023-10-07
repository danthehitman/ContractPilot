using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CPCommon.Data.Configurations;

public class UserAirplaneConfiguration : IEntityTypeConfiguration<UserAirplane>
{
	public void Configure(EntityTypeBuilder<UserAirplane> builder)
	{
		builder.HasKey((UserAirplane ua) => new { ua.UserId, ua.AirplaneId });
		builder.HasOne((UserAirplane ua) => ua.User).WithMany((User u) => u.UserAirplanes).HasForeignKey((UserAirplane ua) => ua.UserId);
	}
}
