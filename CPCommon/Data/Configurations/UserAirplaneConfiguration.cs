using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CPCommon.Data.Configurations
{
    public class UserAirplaneConfiguration : IEntityTypeConfiguration<UserAirplane>
    {
        public void Configure(EntityTypeBuilder<UserAirplane> builder)
        {
            builder.HasKey(ua => new { ua.UserId, ua.AirplaneId });
            builder.HasOne(ua => ua.User).WithMany(u => u.UserAirplanes).HasForeignKey(ua => ua.UserId);
        }
    }
}
