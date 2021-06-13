using CPCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CPCommon.Data.Configurations
{
    class UserAirportConfigurationclass : IEntityTypeConfiguration<UserAirport>
    {
        public void Configure(EntityTypeBuilder<UserAirport> builder)
        {
            builder.HasKey(ua => new { ua.UserId, ua.AirportId });
            builder.HasOne(ua => ua.User).WithMany(u => u.UserAirports).HasForeignKey(ua => ua.UserId);
        }
    }
}
